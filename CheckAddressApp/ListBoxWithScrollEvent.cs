

namespace qAcProviderTest
{
    public class ListBoxWithScrollEvent : ListBox
    {
        public string[] Rows { get { return _rows; } set { _rows = value; Items.Clear(); Items.AddRange(_rows.Take(_count).ToArray()); } }
        public event Action OnScrolledToBottom;

        private string[] _rows;
        private int _currentCount = 40;
        private int _count = 20;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            var endIndex = Items.Count - Size.Height / ItemHeight;

            if (TopIndex == endIndex)
            {
                if (_currentCount <= _rows.Length)
                {
                    var rows = _rows.Take(_currentCount).ToArray();

                    base.Items.AddRange(rows);
                    _currentCount += _count;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x115)
            {
                var type = (ScrollEventType)(m.WParam.ToInt32() & 0xffff);

                if (type == ScrollEventType.EndScroll)
                {
                    var endIndex = Items.Count - Size.Height / ItemHeight;

                    if (TopIndex == endIndex)
                    {
                        if (_currentCount <= _rows.Length)
                        {
                            var rows = _rows.Take(_currentCount).ToArray();

                            base.Items.AddRange(rows);
                            _currentCount += _count;
                        }
                    }
                }
            }
        }
    }
}
