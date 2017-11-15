using System.Collections.Generic;

namespace ListOfWork.Shared.Helpers
{
    public class Notifiable
    {
        private List<Notification> _notifications = new List<Notification>();
        public List<Notification> Notifications => _notifications;

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotification(string property, string message)
        {
            var notification = new Notification(property, message);
            _notifications.Add(notification);
        }

        public void AddNotifications(ICollection<Notification> notifications)
        {
            foreach (var notification in notifications)
                AddNotification(notification);
        }

        public bool IsValid()
        {
            return _notifications.Count == decimal.Zero;
        }
    }
}
