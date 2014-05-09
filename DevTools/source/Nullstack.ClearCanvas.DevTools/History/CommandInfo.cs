using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using ClearCanvas.Desktop;
using Nullstack.ClearCanvas.DevTools.Common;

namespace Nullstack.ClearCanvas.DevTools.History
{
	public class CommandInfo
	{
		private readonly string _name;
		private readonly int _hashcode;
		private readonly string _type;
		private readonly string _assembly;
		//private bool _isCurrent = false;

		private CommandInfo(UndoableCommand command)
		{
			_name = command.Name;
			_hashcode = command.GetHashCode();

			Type type = command.GetType();
			_type = type.FullName;
			_assembly = type.Assembly.FullName;
		}

		//[Browsable(false)]
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//internal bool IsCurrent
		//{
		//    get { return _isCurrent; }
		//    set { _isCurrent = value; }
		//}

		//[Browsable(false)]
		//[EditorBrowsable(EditorBrowsableState.Never)]
		//public string FormattedName
		//{
		//    get { return string.Format(_isCurrent ? SR.fmCurrentCommandName : SR.fmCommandName, this.ToString()); }
		//}

		[Category("Command")]
		[Description("The name of the command.")]
		public string Name
		{
			get { return _name; }
		}

		[Category("Command")]
		[Description("The hash code of the command.")]
		[TypeConverter(typeof (HashcodeConverter))]
		public int Hashcode
		{
			get { return _hashcode; }
		}

		[Category("Type")]
		[Description("The fully qualified type name of the UndoableCommand.")]
		public string Type
		{
			get { return _type; }
		}

		[Category("Type")]
		[Description("The assembly in which the UndoableCommand type is defined.")]
		public string Assembly
		{
			get { return _assembly; }
		}

		[Category("Type")]
		[Description("A value indicating that this command uses the memento pattern.")]
		public virtual bool IsMemorable
		{
			get { return false; }
		}

		[Category("Type")]
		[Description("A value indicating that this command is a composite of other subcommands.")]
		public virtual bool IsComposite
		{
			get { return false; }
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name))
				return string.Format("({0})", this.Type);
			return this.Name;
		}

		internal static CommandInfo CreateCommandInfo(UndoableCommand command)
		{
			if (command is MemorableUndoableCommand)
				return new MemorableCommandInfo((MemorableUndoableCommand) command);
			else if (command is CompositeUndoableCommand)
				return new CompositeCommandInfo((CompositeUndoableCommand) command);
			return new CommandInfo(command);
		}

		private class MemorableCommandInfo : CommandInfo
		{
			private static readonly FieldInfo _rfOriginatorField = typeof(MemorableUndoableCommand).GetField("_originator", BindingFlags.NonPublic | BindingFlags.Instance);
			private readonly IMemorable _originator;
			private readonly object _beginState;
			private readonly object _endState;

			public MemorableCommandInfo(MemorableUndoableCommand command) : base(command)
			{
				_originator = _rfOriginatorField.GetValue(command) as IMemorable;
				_beginState = command.BeginState;
				_endState = command.EndState;
			}

			[Category("Content")]
			[Description("The state of the originator before execution of the command.")]
			[TypeConverter(typeof (ExpandableObjectConverter))]
			public object BeginState
			{
				get { return _beginState; }
			}

			[Category("Content")]
			[Description("The state of the originator after execution of the command.")]
			[TypeConverter(typeof (ExpandableObjectConverter))]
			public object EndState
			{
				get { return _endState; }
			}

			[Category("Content")]
			[Description("The originator of the command.")]
			[TypeConverter(typeof (ExpandableObjectConverter))]
			public IMemorable Originator
			{
				get { return _originator; }
			}

			public override bool IsMemorable
			{
				get { return true; }
			}
		}

		private class CompositeCommandInfo : CommandInfo
		{
			private static readonly FieldInfo _rfCommandsField = typeof (CompositeUndoableCommand).GetField("_commands", BindingFlags.NonPublic | BindingFlags.Instance);
			private readonly int _count;
			private readonly IList<CommandInfo> _commands;

			public CompositeCommandInfo(CompositeUndoableCommand command) : base(command)
			{
				_count = command.Count;

				List<CommandInfo> commands = new List<CommandInfo>();
				List<UndoableCommand> subcommands = _rfCommandsField.GetValue(command) as List<UndoableCommand>;
				if (subcommands != null)
				{
					foreach (UndoableCommand subcommand in subcommands)
						commands.Add(CreateCommandInfo(subcommand));
				}
				_commands = commands.AsReadOnly();
			}

			[Category("Content")]
			[Description("The number of subcommands in this composite command.")]
			public int Count
			{
				get { return _count; }
			}

			[Category("Content")]
			[Description("The subcommands in this composite command.")]
			[ReadOnly(true)]
			[TypeConverter(typeof (ListTypeConverter))]
			public IList<CommandInfo> Commands
			{
				get { return _commands; }
			}

			public override bool IsComposite
			{
				get { return true; }
			}
		}

		private class HashcodeConverter : TypeConverter
		{
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				if (destinationType == typeof (int))
					return true;
				return base.CanConvertTo(context, destinationType);
			}

			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof (string) && value is int)
				{
					uint v = BitConverter.ToUInt32(BitConverter.GetBytes((int) value), 0);
					return string.Format(SR.fmHashcode, v);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}