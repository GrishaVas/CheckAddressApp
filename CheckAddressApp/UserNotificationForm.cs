using CheckAddressApp;

namespace qAcProviderTest
{
    public partial class UserNotificationForm : Form
    {
        private CheckAddressForm _checkAddressForm;

        public UserNotificationForm(CheckAddressForm checkAddressForm)
        {
            _checkAddressForm = checkAddressForm;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            userNotificationRichTextBox.Text = "";
        }

        private void UserNotificationForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                _checkAddressForm.Enabled = true;
            }
            else
            {
                var xLocation = _checkAddressForm.Location.X + _checkAddressForm.Size.Width / 2 - Size.Width / 2;
                var yLocation = _checkAddressForm.Location.Y + _checkAddressForm.Size.Height / 2 - Size.Height / 2;

                Location = new Point(xLocation, yLocation);

                _checkAddressForm.Enabled = false;
            }
        }
    }
}
