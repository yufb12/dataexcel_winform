using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Feng.Excel.Commands
{
    public class Command
    {
        public CommandType CommandType
        {
            get;
            set;
        }

        public DataUrl Url
        {
            get;
            set;
        }
    }

    public enum CommandType
    {
        OK,
        Cancel,

    }

    public class DataUrl
    {
        public SubmitType SubmitType { get; set; }
        public string Src { get; set; }
    }

    public enum SubmitType
    {
        Post,
        Send
    }
}
