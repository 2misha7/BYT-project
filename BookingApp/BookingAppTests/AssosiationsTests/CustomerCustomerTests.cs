using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class CustomerCustomerTests
{
    [Test]
    public void Test_AddInvitedCustomer_Successful()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var invitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());

        inviter.AddInvitedCustomer(invitee);

        Assert.AreEqual(1, inviter.InvitedCustomers.Count);
        Assert.AreEqual(inviter, invitee.Inviter);
        Assert.AreEqual(invitee, inviter.InvitedCustomers[0]);
    }
    [Test]
    public void Test_AddInvitedCustomer_ExceptionHandling()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var invitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());

        inviter.AddInvitedCustomer(invitee);

        // Null customer
        var ex1 = Assert.Throws<ArgumentNullException>(() => inviter.AddInvitedCustomer(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'customer')", ex1.Message);

        // Customer invites themselves
        var ex2 = Assert.Throws<InvalidOperationException>(() => inviter.AddInvitedCustomer(inviter));
        Assert.AreEqual("It is not possible for customers to invite themselves.", ex2.Message);

        // Customer already invited
        var ex3 = Assert.Throws<InvalidOperationException>(() => inviter.AddInvitedCustomer(invitee));
        Assert.AreEqual("This Customer has already invited this new Customer.", ex3.Message);

        // Invitee has already invited inviter
        var ex4 = Assert.Throws<InvalidOperationException>(() => invitee.AddInvitedCustomer(inviter));
        Assert.AreEqual("It is not possible to invite a customer who has invited you.", ex4.Message);
    }
    
    [Test]
        public void Test_AddInviter_Successful()
        {
            var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johnDoe", "password", "123 Main St", "City1", 100m, new RegularAccountType());
            var invitee = new Customer("Jane", "Doe", "jane.doe@example.com", "+7987654321", "janeDoe", "password", "456 Elm St", "City2", 150m, new RegularAccountType());

            invitee.AddInviter(inviter);

            Assert.AreEqual(inviter, invitee.Inviter);
            Assert.Contains(invitee, inviter.InvitedCustomers.ToList());
        }

        [Test]
        public void Test_AddInviter_ExceptionHandling()
        {
            var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johnDoe", "password", "123 Main St", "City1", 100m, new RegularAccountType());
            var invitee = new Customer("Jane", "Doe", "jane.doe@example.com", "+487654321", "janeDoe", "password", "456 Elm St", "City2", 150m, new RegularAccountType());

            invitee.AddInviter(inviter);

            // Null inviter
            var ex1 = Assert.Throws<ArgumentNullException>(() => invitee.AddInviter(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'customer')", ex1.Message);

            // Self-invitation
            var ex2 = Assert.Throws<InvalidOperationException>(() => invitee.AddInviter(invitee));
            Assert.AreEqual("It is not possible for customers to invite themselves.", ex2.Message);

            // Inviter already invited invitee
            var ex3 = Assert.Throws<InvalidOperationException>(() => inviter.AddInviter(invitee));
            Assert.AreEqual("It is not possible to be invited by a Customer who has invited you.", ex3.Message);

            // Invitee already has an inviter
            var newInviter = new Customer("Jake", "Smith", "jake.smith@example.com", "+1122334455", "jakeSmith", "password", "789 Oak St", "City3", 200m, new RegularAccountType());
            var ex4 = Assert.Throws<InvalidOperationException>(() => invitee.AddInviter(newInviter));
            Assert.AreEqual("This Customer alread has an inviter.", ex4.Message);
        }
    [Test]
    public void Test_RemoveInvitedCustomer_Successful()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var invitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());

        inviter.AddInvitedCustomer(invitee);
        inviter.RemoveInvitedCustomer(invitee);

        Assert.AreEqual(0, inviter.InvitedCustomers.Count);
        Assert.IsNull(invitee.Inviter);
    }
    
    [Test]
    public void Test_RemoveInvitedCustomer_ExceptionHandling()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var invitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());

        // Removing a customer not invited
        var ex1 = Assert.Throws<InvalidOperationException>(() => inviter.RemoveInvitedCustomer(invitee));
        Assert.AreEqual("This Customer hasn't invited this new Customer.", ex1.Message);
    }
    
    [Test]
    public void Test_RemoveInviter_Successful()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johnDoe", "password", "123 Main St", "City1", 100m, new RegularAccountType());
        var invitee = new Customer("Jane", "Doe", "jane.doe@example.com", "+987654321", "janeDoe", "password", "456 Elm St", "City2", 150m, new RegularAccountType());

        invitee.AddInviter(inviter);
        invitee.RemoveInviter();

        Assert.IsNull(invitee.Inviter);
        Assert.IsFalse(inviter.InvitedCustomers.Contains(invitee));
    }

    [Test]
    public void Test_RemoveInviter_ExceptionHandling()
    {
        var invitee = new Customer("Jane", "Doe", "jane.doe@example.com", "+987654321", "janeDoe", "password", "456 Elm St", "City2", 150m, new RegularAccountType());

        // Removing inviter when no inviter is assigned
        var ex = Assert.Throws<InvalidOperationException>(() => invitee.RemoveInviter());
        Assert.AreEqual("This Customer does not have an inviter", ex.Message);
    }
    [Test]
    public void Test_ChangeInviter_Successful()
    {
        var originalInviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var newInviter = new Customer("Alice", "Johnson", "alice.johnson@example.com", "+1234567890", "alicejohnson", "password", "789 Oak St", "Chicago", 300m, new RegularAccountType());
        var invitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());

        originalInviter.AddInvitedCustomer(invitee);
        invitee.ChangeInviter(newInviter);

        Assert.AreEqual(0, originalInviter.InvitedCustomers.Count);
        Assert.AreEqual(newInviter, invitee.Inviter);
        Assert.AreEqual(1, newInviter.InvitedCustomers.Count);
        Assert.AreEqual(invitee, newInviter.InvitedCustomers[0]);
    }
    [Test]
    public void Test_ChangeInviter_ExceptionHandling()
    {
        var originalInviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var newInviter = new Customer("Alice", "Johnson", "alice.johnson@example.com", "+1234567890", "alicejohnson", "password", "789 Oak St", "Chicago", 300m, new RegularAccountType());
        var invitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());

        // Change inviter for a customer with no inviter
        var ex1 = Assert.Throws<InvalidOperationException>(() => invitee.ChangeInviter(newInviter));
        Assert.AreEqual("It is not possible to assign new inviter to this Customer, because it has not been invited to the system, by any", ex1.Message);

        // Change inviter to null
        originalInviter.AddInvitedCustomer(invitee);
        var ex2 = Assert.Throws<ArgumentNullException>(() => invitee.ChangeInviter(null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newInviter')", ex2.Message);

        // Change inviter to the same inviter
        var ex3 = Assert.Throws<InvalidOperationException>(() => invitee.ChangeInviter(originalInviter));
        Assert.AreEqual("This Customer has already been invited by exactly this inviter", ex3.Message);
    }
    
    [Test]
    public void Test_SubstituteInvitedCustomer_Successful()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var oldInvitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());
        var newInvitee = new Customer("Alice", "Johnson", "alice.johnson@example.com", "+1234567890", "alicejohnson", "password", "789 Oak St", "Chicago", 300m, new RegularAccountType());

        inviter.AddInvitedCustomer(oldInvitee);
        inviter.SubstituteInvitedCustomer(oldInvitee, newInvitee);

        Assert.AreEqual(1, inviter.InvitedCustomers.Count);
        Assert.AreEqual(newInvitee, inviter.InvitedCustomers[0]);
        Assert.IsNull(oldInvitee.Inviter);
        Assert.AreEqual(inviter, newInvitee.Inviter);
    }
    
    [Test]
    public void Test_SubstituteInvitedCustomer_ExceptionHandling()
    {
        var inviter = new Customer("John", "Doe", "john.doe@example.com", "+1234567890", "johndoe", "password", "123 Main St", "New York", 100m, new RegularAccountType());
        var oldInvitee = new Customer("Jane", "Smith", "jane.smith@example.com", "+1234567890", "janesmith", "password", "456 Elm St", "Los Angeles", 200m, new RegularAccountType());
        var newInvitee = new Customer("Alice", "Johnson", "alice.johnson@example.com", "+1234567890", "alicejohnson", "password", "789 Oak St", "Chicago", 300m, new RegularAccountType());

        // Substitute with null
        var ex1 = Assert.Throws<ArgumentNullException>(() => inviter.SubstituteInvitedCustomer(null, newInvitee));
        Assert.AreEqual("Value cannot be null. (Parameter 'oldC')", ex1.Message);

        var ex2 = Assert.Throws<ArgumentNullException>(() => inviter.SubstituteInvitedCustomer(oldInvitee, null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newC')", ex2.Message);

        // Substitute a customer not invited
        var ex3 = Assert.Throws<Exception>(() => inviter.SubstituteInvitedCustomer(oldInvitee, newInvitee));
        Assert.AreEqual("This Customer has not invited this old Customer", ex3.Message);

        inviter.AddInvitedCustomer(oldInvitee);

        // Substitute with a customer already invited
        inviter.AddInvitedCustomer(newInvitee);
        var ex4 = Assert.Throws<Exception>(() => inviter.SubstituteInvitedCustomer(oldInvitee, newInvitee));
        Assert.AreEqual("This Customer has already invited this new Customer", ex4.Message);
    }

}