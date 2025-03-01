using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OOO3.Program;

namespace OOO3
{
    internal class InterfaceQuality : BaseClass
    {
        public string Value;
        public SensualObject? SOFrom;
        public SensualObject? SOTo;
        public InterfaceCommands InterfaceCommand;
        //public SensualObject? SOInherit;
        public string SOInherit;
        public InterfaceQuality(string _name, string _value, string _description, InterfaceCommands _interfaceCommand)
        {
            Name = _name;
            Description = _description;
            Value = _value;
            InterfaceCommand = _interfaceCommand;
            SOInherit = "";
        }

        public InterfaceQuality(string _name, string _value, string _description, InterfaceCommands _interfaceCommand, string _SOInherit)
        {
            Name = _name;
            Description = _description;
            Value = _value;
            InterfaceCommand = _interfaceCommand;
            SOInherit = _SOInherit;
         }

        public InterfaceQuality(string _name, string _value, string _description, InterfaceCommands _interfaceCommand, DateTime _created)
        {
            Name = _name;
            Description = _description;
            Value = _value;
            InterfaceCommand = _interfaceCommand;
            //SOInherit = _SOInherit;
            created = _created;
        }

        public InterfaceQuality(SensualObject _SOFrom, string _name, string _value, SensualObject? _SOTo, string _description, InterfaceCommands _interfaceCommand, DateTime _created)
        {
            SOFrom = _SOFrom;
            Name = _name;
            Value = _value;
            SOTo = _SOTo;
            Description = _description;
            InterfaceCommand = _interfaceCommand;
            SOInherit = "";
            created = _created;
        }
    }

        internal class InterfaceEvent : BaseClass
        {

            protected internal List<InterfaceQuality> qualities = new List<InterfaceQuality>();
            public virtual void AddQuality(string _name, string _value, string _description, InterfaceCommands _interfaceCommand, string _SOInherit)
            {
                InterfaceQuality IQ = new(_name, _value, _description, _interfaceCommand, _SOInherit);
                qualities.Add(IQ);
            SOs.processIO(this);
        }
        public virtual void AddQuality(string _name, string _value, string _description, InterfaceCommands _interfaceCommand)
        {
            InterfaceQuality IQ = new(_name, _value, _description, _interfaceCommand, null);
            qualities.Add(IQ);
            SOs.processIO(this);
        }
        public virtual void AddQuality(string _name, string _value, string _description)
        {
            InterfaceQuality IQ = new(_name, _value, _description, InterfaceCommands.NONE, this.created);
            qualities.Add(IQ);
            SOs.processIO(this);
        }

        public virtual void AddQuality(string _name, string _value, string _description, DateTime created)
            {
                InterfaceQuality IQ = new(_name, _value, _description, InterfaceCommands.NONE, created);
                qualities.Add(IQ);
            SOs.processIO(this);
        }

            public virtual void AddQuality(SensualObject _SOFrom, string _name, string _value, SensualObject? _SOTo, string _description, DateTime created)
            {

                InterfaceQuality IQ = new(_SOFrom, _name, _value, _SOTo, _description, InterfaceCommands.NONE, created);
                qualities.Add(IQ);
            SOs.processIO(this);

        }

            public InterfaceEvent(string _name, string _description, DateTime _dt)
            {
                Name = _name;
                Description = _description;
                this.created = _dt;
            }
        }
    }

