namespace ComputerStore.Domain.ValueObject
{
    public class EmailAddress
    {
        public string Value { get; }
        public EmailAddress(string value) { 
            if (value == null) 
                throw new ArgumentNullException("Enter value");
            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email address", nameof(value));
            Value = value;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
