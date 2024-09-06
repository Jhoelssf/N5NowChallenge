using System.Net;
using N5Application.Classes;

namespace N5Application.Models
{
    public class Response<T>
    {
        private readonly List<Notify> _notifications = new();
        private readonly Dictionary<string, string> _headers = new();

        public T? Content { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public IReadOnlyList<Notify> Notifications => _notifications;

        public bool IsValid => !_notifications.Any();

        public IReadOnlyDictionary<string, string> Headers => _headers;

        public void AddNotifications(IEnumerable<Notify> notifies)
        {
            _notifications.AddRange(notifies);
        }

        public void AddNotification(Notify notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotification(string code, string property, string message)
        {
            _notifications.Add(new Notify
            {
                Code = code,
                Message = message,
                Property = property
            });
        }

        public void AddHeader(string name, string value)
        {
            _headers[name] = value;
        }
    }
}