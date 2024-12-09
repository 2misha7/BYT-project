namespace BookingApp.Models;

public class Notification: ModelBase<Notification>
{
    private int _id;
    

    public int Id
    {
        get => _id;
        private set => _id = value;
    }
    private string _text = null!;
    public string Text
    {
        get => _text;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Text cannot be empty");
            }
            _text = value;
        }
    }

    public Notification(string text)
    {
        try
        {
            Text = text;
            Add(this);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    protected override void AssignId()
    {
        Id = GetAll().Count > 0 ? GetAll().Last().Id + 1 : 1;
    }
    
    // One-to-One Relationship with Booking
    private Booking? _booking;
    public Booking? Booking => _booking;

    private bool _isUpdating = false;
    public void AddBookingToNotification(Booking booking)
    {
        if (booking == null)
            throw new ArgumentNullException(nameof(booking));

        if (_isUpdating)
        {
            return;
        }

        if (_booking != null)
        {
            throw new InvalidOperationException("This Notification is already assigned to a Booking.");
        }

        _isUpdating = true;
        _booking = booking;
        booking.AddNotificationToBooking(this);
        _isUpdating = false;
    }
    
    public void RemoveNotificationFromBooking()
    {
        if (_isUpdating) return;
        if (_booking == null) 
            throw new InvalidOperationException("This notification does not have a booking");
        _isUpdating = true;
        var previousBooking = _booking;
        _booking= null;
        previousBooking.RemoveBookingFromNotification(); 
        _isUpdating = false;
        
    }
    
    public void ChangeBookingInNotification(Booking newBooking)
    {
        if (newBooking == null)
            throw new ArgumentNullException(nameof(newBooking));
        if (_booking == newBooking)
        {
            throw new InvalidOperationException("This Notification is already assigned to this Booking");
        }

        if (_booking == null)
        {
            throw new InvalidOperationException(
                "It is not possible to assign a new Booking to this Notification, because it does not have any");
        }
        RemoveNotificationFromBooking(); 
        AddBookingToNotification(newBooking); 
    }
}