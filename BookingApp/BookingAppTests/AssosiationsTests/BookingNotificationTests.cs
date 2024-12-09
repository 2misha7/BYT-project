using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class BookingNotificationTests
{
    [Test]
    public void Test_AddNotificationToBooking()
    {
        var booking = new Booking();
        var notification = new Notification("Booking confirmed!");

        booking.AddNotificationToBooking(notification);

        Assert.AreEqual(notification, booking.Notification);
        Assert.AreEqual(booking, notification.Booking);
    }

    [Test]
    public void Test_AddNotificationToBooking_ExceptionHandling()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var notification1 = new Notification("Booking confirmed!");
        var notification2 = new Notification("Booking updated!");

        booking1.AddNotificationToBooking(notification1);

        var ex1 = Assert.Throws<ArgumentNullException>(() => booking2.AddNotificationToBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'notification')", ex1.Message);
        
        // Adding a notification to a booking that already has one should throw an exception
        var ex = Assert.Throws<InvalidOperationException>(() => booking1.AddNotificationToBooking(notification2));
        Assert.AreEqual("This Booking already has a Notification.", ex.Message);
        
    }

    [Test]
    public void Test_AddBookingToNotification()
    {
        var booking = new Booking();
        var notification = new Notification("Booking confirmed!");

        notification.AddBookingToNotification(booking);

        Assert.AreEqual(booking, notification.Booking);
        Assert.AreEqual(notification, booking.Notification);
    }

    [Test]
    public void Test_AddBookingToNotification_ExceptionHandling()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var notification1 = new Notification("Booking confirmed!");
        var notification2 = new Notification("Booking updated!");
        
        var ex1 = Assert.Throws<ArgumentNullException>(() => notification1.AddBookingToNotification(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'booking')", ex1.Message);

        notification1.AddBookingToNotification(booking1);
        
        var ex = Assert.Throws<InvalidOperationException>(() => notification1.AddBookingToNotification(booking2));
        Assert.AreEqual("This Notification is already assigned to a Booking.", ex.Message);
        
    }
    
    [Test]
    public void Test_RemoveBookingFromNotification_StartedInNotification()
    {
        var notification = new Notification("Notification");
        var booking = new Booking();
        
        notification.AddBookingToNotification(booking);
        notification.RemoveNotificationFromBooking();
        
        Assert.IsNull(booking.Notification);
        Assert.IsNull(notification.Booking);
    }
    
    [Test]
    public void Test_RemoveBookingFromNotification_ExceptionHandling()
    {
        var notification = new Notification("Booking confirmed!");
        
        var ex1 = Assert.Throws<InvalidOperationException>(() => notification.RemoveNotificationFromBooking());
        Assert.AreEqual("This notification does not have a booking", ex1.Message);
    }
    
    [Test]
    public void Test_NotificationFromBooking_StartedInBooking()
    {
        var notification = new Notification("Notification");
        var booking = new Booking();
        
        booking.AddNotificationToBooking(notification);
        booking.RemoveBookingFromNotification();
        
        Assert.IsNull(booking.Notification);
        Assert.IsNull(notification.Booking);
    }
    [Test]
    public void Test_NotificationFromBooking_ExceptionHandling()
    {
        var booking = new Booking();
        
        var ex1 = Assert.Throws<InvalidOperationException>(() => booking.RemoveBookingFromNotification());
        Assert.AreEqual("This booking does not have a notification", ex1.Message);
    }
    
    
    [Test]
    public void Test_ChangeNotificationInBooking_Success()
    {
        var booking = new Booking();
        var notification1 = new Notification("Notification 1");
        var notification2 = new Notification("Notification 2");

        booking.AddNotificationToBooking(notification1);
        booking.ChangeNotificationInBooking(notification2);

        Assert.AreEqual(notification2, booking.Notification);
        Assert.AreEqual(booking, notification2.Booking);
        Assert.IsNull(notification1.Booking);
    }

    [Test]
    public void Test_ChangeNotificationInBooking_ExceptionHandling()
    {
        var booking = new Booking();
        var notification1 = new Notification("Notification 1");
        var notification2 = new Notification("Notification 2");

        var ex1 = Assert.Throws<ArgumentNullException>(() => booking.ChangeNotificationInBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newNotification')", ex1.Message);

        booking.AddNotificationToBooking(notification1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => booking.ChangeNotificationInBooking(notification1));
        Assert.AreEqual("This Booking already has exactly this Notification", ex2.Message);

        booking.RemoveBookingFromNotification();

        var ex3 = Assert.Throws<InvalidOperationException>(() => booking.ChangeNotificationInBooking(notification2));
        Assert.AreEqual("It is not possible to assign a new notification to this Booking, because it does not have any", ex3.Message);
    }
    
    [Test]
    public void Test_ChangeBookingInNotification()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var notification = new Notification("Notification Text");

        notification.AddBookingToNotification(booking1);
        notification.ChangeBookingInNotification(booking2);

        Assert.AreEqual(booking2, notification.Booking);
        Assert.AreEqual(notification, booking2.Notification);
        Assert.IsNull(booking1.Notification);
    }

    [Test]
    public void Test_ChangeBookingInNotification_ExceptionHandling()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var notification = new Notification("Notification Text");

        var ex1 = Assert.Throws<ArgumentNullException>(() => notification.ChangeBookingInNotification(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newBooking')", ex1.Message);

        notification.AddBookingToNotification(booking1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => notification.ChangeBookingInNotification(booking1));
        Assert.AreEqual("This Notification is already assigned to this Booking", ex2.Message);

        notification.RemoveNotificationFromBooking();

        var ex3 = Assert.Throws<InvalidOperationException>(() => notification.ChangeBookingInNotification(booking2));
        Assert.AreEqual("It is not possible to assign a new Booking to this Notification, because it does not have any", ex3.Message);
    }
    
}