using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System.Collections.Generic;
using System.Text;

namespace Feng.Commands
{
    //规则
    // 名称:参数,参数
    //  参数：数字，字符 

    public delegate void CommandTextHandler(object sender, string name, string[] args);

    public interface ICommandText : IName, IParamsText
    {

    }
 
}
