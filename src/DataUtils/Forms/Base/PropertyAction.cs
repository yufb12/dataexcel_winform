namespace Feng.Forms.Base
{
    public class PropertyAction:Interface.IPropertyAction
    {
        public PropertyAction()
        {

        }

        public virtual string ActionName { get; set; }
        public virtual string Descript { get; set; }
        public virtual string Script { get; set; }
        public virtual string ShortName { get; set; }
    }
}