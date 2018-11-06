using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator_Engine_Test
{
    public class ListItem
    {
        private string _value = string.Empty;
        private string _text = string.Empty;
        private string _remark = string.Empty;

        public override string ToString()
        {
            return this._text;
        }
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
        public ListItem()
        { }
        public ListItem(string strValue, string strText, string strRemark)
        {
            _value = strValue;
            _text = strText;
            _remark = strRemark;
        }
    }
}
