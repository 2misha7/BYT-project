using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class BookingPaymentTests
{
     [Test]
    public void Test_AddPaymentToBooking_StartedInBooking()
    {
        var booking = new Booking();
        var payment = new Payment(100m, null);

        booking.AddPaymentToBooking(payment);

        Assert.AreEqual(payment, booking.Payment);
        Assert.AreEqual(booking, payment.Booking);
    }

    [Test]
    public void Test_AddPaymentToBooking_ExceptionHandling()
    {
        var booking = new Booking();
        var payment1 = new Payment(100m, null);
        var payment2 = new Payment(200m, null);

        var ex1 = Assert.Throws<ArgumentNullException>(() => booking.AddPaymentToBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'payment')", ex1.Message);

        booking.AddPaymentToBooking(payment1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => booking.AddPaymentToBooking(payment2));
        Assert.AreEqual("This Booking already has a Payment.", ex2.Message);
    }

    [Test]
    public void Test_RemovePaymentFromBooking_StartedInBooking()
    {
        var booking = new Booking();
        var payment = new Payment(100m, null);

        booking.AddPaymentToBooking(payment);
        booking.RemovePaymentFromBooking();

        Assert.IsNull(booking.Payment);
        Assert.IsNull(payment.Booking);
    }

    [Test]
    public void Test_RemovePaymentFromBooking_ExceptionHandling()
    {
        var booking = new Booking();

        var ex = Assert.Throws<InvalidOperationException>(() => booking.RemovePaymentFromBooking());
        Assert.AreEqual("This Booking does not have a Payment", ex.Message);
    }

    [Test]
    public void Test_ChangePaymentForBooking_StartedInBooking()
    {
        var booking = new Booking();
        var payment1 = new Payment(100m, null);
        var payment2 = new Payment(200m, null);

        booking.AddPaymentToBooking(payment1);
        booking.ChangePaymentForBooking(payment2);

        Assert.AreEqual(payment2, booking.Payment);
        Assert.AreEqual(booking, payment2.Booking);
        Assert.IsNull(payment1.Booking);
    }

    [Test]
    public void Test_ChangePaymentForBooking_ExceptionHandling()
    {
        var booking = new Booking();
        var payment1 = new Payment(100m, null);
        var payment2 = new Payment(200m, null);

        var ex1 = Assert.Throws<ArgumentNullException>(() => booking.ChangePaymentForBooking(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newPayment')", ex1.Message);

        booking.AddPaymentToBooking(payment1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => booking.ChangePaymentForBooking(payment1));
        Assert.AreEqual("This Payment is already assigned to this Booking", ex2.Message);

        booking.RemovePaymentFromBooking();

        var ex3 = Assert.Throws<InvalidOperationException>(() => booking.ChangePaymentForBooking(payment2));
        Assert.AreEqual("It is not possible to assign a new Payment to this Booking, because it does not have any", ex3.Message);
    }

    [Test]
    public void Test_AddBookingToPayment_StartedInPayment()
    {
        var booking = new Booking();
        var payment = new Payment(100m, null);

        payment.AddBookingToPayment(booking);

        Assert.AreEqual(booking, payment.Booking);
        Assert.AreEqual(payment, booking.Payment);
    }

    [Test]
    public void Test_AddBookingToPayment_ExceptionHandling()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var payment = new Payment(100m, null);

        var ex1 = Assert.Throws<ArgumentNullException>(() => payment.AddBookingToPayment(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'booking')", ex1.Message);

        payment.AddBookingToPayment(booking1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => payment.AddBookingToPayment(booking2));
        Assert.AreEqual("This Payment is already assigned to a Booking.", ex2.Message);
    }

    [Test]
    public void Test_RemoveBookingFromPayment_StartedInPayment()
    {
        var booking = new Booking();
        var payment = new Payment(100m, null);

        payment.AddBookingToPayment(booking);
        payment.RemoveBookingFromPayment();

        Assert.IsNull(payment.Booking);
        Assert.IsNull(booking.Payment);
    }

    [Test]
    public void Test_RemoveBookingFromPayment_ExceptionHandling()
    {
        var payment = new Payment(100m, null);

        var ex = Assert.Throws<InvalidOperationException>(() => payment.RemoveBookingFromPayment());
        Assert.AreEqual("This Payment does not have a booking", ex.Message);
    }

    [Test]
    public void Test_ChangeBookingForThisPayment_StartedInPayment()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var payment = new Payment(100m, null);

        payment.AddBookingToPayment(booking1);
        payment.ChangeBookingForThisPayment(booking2);

        Assert.AreEqual(booking2, payment.Booking);
        Assert.AreEqual(payment, booking2.Payment);
        Assert.IsNull(booking1.Payment);
    }

    [Test]
    public void Test_ChangeBookingForThisPayment_ExceptionHandling()
    {
        var booking1 = new Booking();
        var booking2 = new Booking();
        var payment = new Payment(100m, null);

        var ex1 = Assert.Throws<ArgumentNullException>(() => payment.ChangeBookingForThisPayment(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newBooking')", ex1.Message);

        payment.AddBookingToPayment(booking1);

        var ex2 = Assert.Throws<InvalidOperationException>(() => payment.ChangeBookingForThisPayment(booking1));
        Assert.AreEqual("This Payment is already assigned to this Booking", ex2.Message);

        payment.RemoveBookingFromPayment();

        var ex3 = Assert.Throws<InvalidOperationException>(() => payment.ChangeBookingForThisPayment(booking2));
        Assert.AreEqual("It is not possible to assign a new Booking to this Payment, because it does not have any", ex3.Message);
    }
}