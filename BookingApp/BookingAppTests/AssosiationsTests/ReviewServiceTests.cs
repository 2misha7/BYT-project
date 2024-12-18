using BookingApp.Models;

namespace BookingAppTests.AssosiationsTests;

public class ReviewServiceTests
{
     [Test]
        public void Test_AddReviewToService_StartedInService()
        {
            var service = new Service("Haircut", StationCategory.Hair, "A quick trim", 25.0m);
            var review = new Review(ReviewRating.Good, "Great haircut!", DateTime.Now);

            service.AddReviewToService(review);

            Assert.AreEqual(1, service.Reviews.Count);
            Assert.AreEqual(service, review.Service);
            Assert.AreEqual(review, service.Reviews[0]);
        }

        [Test]
        public void Test_AddReviewToService_StartedInService_ExceptionHandling()
        {
            var service = new Service("Haircut", StationCategory.Hair, "A quick trim", 25.0m);
            var review = new Review(ReviewRating.Good, "Great haircut!", DateTime.Now);

            // Null Review
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.AddReviewToService(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'review')", ex1.Message);

            // Duplicate Review
            service.AddReviewToService(review);
            var ex2 = Assert.Throws<InvalidOperationException>(() => service.AddReviewToService(review));
            Assert.AreEqual("This Service already has this Review.", ex2.Message);
        }

        // Add Service to Review - started in Review
        [Test]
        public void Test_AddServiceToReview_StartedInReview()
        {
            var service = new Service("Manicure", StationCategory.Nail, "Deluxe manicure", 30.0m);
            var review = new Review(ReviewRating.Perfect, "My nails look amazing!", DateTime.Now);

            review.AssignServiceToReview(service);

            Assert.AreEqual(1, service.Reviews.Count);
            Assert.AreEqual(service, review.Service);
            Assert.AreEqual(review, service.Reviews[0]);
        }

        [Test]
        public void Test_AddServiceToReview_StartedInReview_ExceptionHandling()
        {
            var service = new Service("Manicure", StationCategory.Nail, "Deluxe manicure", 30.0m);
            var review = new Review(ReviewRating.Perfect, "My nails look amazing!", DateTime.Now);

            // Null Service
            var ex1 = Assert.Throws<ArgumentNullException>(() => review.AssignServiceToReview(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'service')", ex1.Message);

            // Duplicate Assignment
            review.AssignServiceToReview(service);
            var ex2 = Assert.Throws<InvalidOperationException>(() => review.AssignServiceToReview(service));
            Assert.AreEqual("This Review is already assigned to a Service in the system.", ex2.Message);
        }

        // Remove Review from Service - started in Service
        [Test]
        public void Test_RemoveReviewFromService_StartedInService()
        {
            var service = new Service("Massage", StationCategory.Body, "Relaxing massage", 50.0m);
            var review = new Review(ReviewRating.VeryGood, "Best massage ever!", DateTime.Now);

            service.AddReviewToService(review);
            service.RemoveReviewFromService(review);

            Assert.AreEqual(0, service.Reviews.Count);
            Assert.IsNull(review.Service);
        }

        [Test]
        public void Test_RemoveReviewFromService_StartedInService_ExceptionHandling()
        {
            var service = new Service("Massage", StationCategory.Body, "Relaxing massage", 50.0m);
            var review = new Review(ReviewRating.VeryGood, "Best massage ever!", DateTime.Now);

            // Null Review
            var ex1 = Assert.Throws<ArgumentNullException>(() => service.RemoveReviewFromService(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'review')", ex1.Message);

            // Review not part of Service
            var ex2 = Assert.Throws<InvalidOperationException>(() => service.RemoveReviewFromService(review));
            Assert.AreEqual("This Service does not have this Review.", ex2.Message);
        }
        
        [Test]
        public void Test_RemoveServiceFromReview()
        {
            var service = new Service("Massage", StationCategory.Body, "Relaxing massage", 50.0m);
            var review = new Review(ReviewRating.VeryGood, "Best massage ever!", DateTime.Now);

            review.AssignServiceToReview(service);
            review.RemoveServiceFromReview();

            Assert.AreEqual(0, service.Reviews.Count);
            Assert.IsNull(review.Service);
        }

        [Test]
        public void Test_RemovePortfolioPageFromPost_ExceptionHandling()
        {
            var review = new Review(ReviewRating.VeryGood, "Best massage ever!", DateTime.Now);

            var ex = Assert.Throws<InvalidOperationException>(() => review.RemoveServiceFromReview());
            Assert.AreEqual("This Review is not assigned to a Service", ex.Message);
        }
        
        
        [Test]
    public void Test_SubstituteReview_Successful()
    {
        // Arrange
        var service = new Service("Haircut", StationCategory.Hair, "A classic haircut service", 30.0m);
        var oldReview = new Review(ReviewRating.Good, "Nice service!", DateTime.Now);
        var newReview = new Review(ReviewRating.Perfect, "Amazing experience!", DateTime.Now);

        service.AddReviewToService(oldReview);

        // Act
        service.SubstituteReview(oldReview, newReview);

        // Assert
        Assert.AreEqual(1, service.Reviews.Count, "Service should have exactly one review after substitution.");
        Assert.AreEqual(newReview, service.Reviews[0], "The new review should replace the old review in the service.");
        Assert.IsNull(oldReview.Service, "The old review should no longer be associated with the service.");
        Assert.AreEqual(service, newReview.Service, "The new review should be correctly associated with the service.");
    }

    [Test]
    public void Test_SubstituteReview_ExceptionHandling()
    {
        // Arrange
        var service = new Service("Massage", StationCategory.Body, "Relaxing massage therapy", 50.0m);
        var oldReview = new Review(ReviewRating.Good, "Nice experience.", DateTime.Now);
        var newReview = new Review(ReviewRating.Perfect, "Amazing service!", DateTime.Now);
        var externalReview = new Review(ReviewRating.Medium, "Average service.", DateTime.Now);
        var anotherService = new Service("Manicure", StationCategory.Nail, "A deluxe manicure", 40.0m);

        service.AddReviewToService(oldReview);
        anotherService.AddReviewToService(externalReview);

        // Act & Assert

        // Case 1: Null arguments
        var ex1 = Assert.Throws<ArgumentNullException>(() => service.SubstituteReview(null, newReview));
        Assert.AreEqual("Value cannot be null. (Parameter 'oldReview')", ex1.Message);

        var ex2 = Assert.Throws<ArgumentNullException>(() => service.SubstituteReview(oldReview, null));
        Assert.AreEqual("Value cannot be null. (Parameter 'newReview')", ex2.Message);

        // Case 2: Old review not in service
        var ex3 = Assert.Throws<Exception>(() => service.SubstituteReview(externalReview, newReview));
        Assert.AreEqual("This Service does not have this Review", ex3.Message);

        // Case 3: New review already in service
        service.AddReviewToService(newReview);
        var ex4 = Assert.Throws<Exception>(() => service.SubstituteReview(oldReview, newReview));
        Assert.AreEqual("This Service already has this Review", ex4.Message);

        // Case 4: New review assigned to another service
        var ex5 = Assert.Throws<Exception>(() => service.SubstituteReview(oldReview, externalReview));
        Assert.AreEqual("It is not possible to add this Review to a Service, as it is already assigned to a Service in the system", ex5.Message);
    }
    
    [Test]
    public void Test_DeleteService_WillDeleteReviews()
    {
        var service = new Service("Massage", StationCategory.Body, "Relaxing massage therapy", 50.0m);
        var oldReview = new Review(ReviewRating.Good, "Nice experience.", DateTime.Now);
        var newReview = new Review(ReviewRating.Perfect, "Amazing service!", DateTime.Now);

        service.AddReviewToService(oldReview);
        service.AddReviewToService(newReview);
        
        service.DeleteService();

        Assert.IsNull(oldReview.Service);
        Assert.IsNull(newReview.Service);
        Assert.IsFalse(Service.GetAll().Contains(service));
        Assert.IsFalse(Review.GetAll().Contains(oldReview));
        Assert.IsFalse(Review.GetAll().Contains(newReview));
    }

    [Test]
    public void Test_DeleteReview_RemovesItFromService()
    {
        var service = new Service("Massage", StationCategory.Body, "Relaxing massage therapy", 50.0m);
        var oldReview = new Review(ReviewRating.Good, "Nice experience.", DateTime.Now);
        
        service.AddReviewToService(oldReview);
        oldReview.DeleteReview();

        Assert.IsNull(oldReview.Service);
        Assert.AreEqual(0, service.Reviews.Count);
        Assert.IsFalse(Review.GetAll().Contains(oldReview));
    }
    
}