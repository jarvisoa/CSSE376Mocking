using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod()]
        public void TestThatCarDoesGetLocationFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String location = "Seattle";
            Expect.Call(mockDB.getCarLocation(2)).Return(location);
            mocks.ReplayAll();
            Car target = new Car(10);


            target.Database = mockDB;
            String result;
            result = target.getCarLocation(2);
            Assert.AreEqual(location, result);
            mocks.VerifyAll();

        }


        [TestMethod()]
        public void TestThatCarDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int expectedMileage = 200;

            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();
            mockDatabase.Miles = expectedMileage;
            var target = new Car(10);
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(expectedMileage, mileage );

            mocks.VerifyAll();

        }

        [TestMethod]
        public void TestThatCarHasCorrectBasePriceForTenDaysUsingObjectMother()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(80, target.getBasePrice());
        }
	}
}
