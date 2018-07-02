using CampingReservations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReservationTests
{
    /// <summary>
    /// Tests to ensure program handles incorrect files
    /// </summary>
    [TestClass]
    public class ReservationTests
    {
        /// <summary>
        /// Empty JSON file resturns false
        /// </summary>
        [TestMethod]
        public void Empty_JSON_Returns_False()
        {
            JSONData data = new JSONData();
            var error = data.CreateData("..\\..\\JSONTestFiles\\Empty.json");
            Assert.AreEqual(error, false);
        }

        /// <summary>
        /// No search parameters in JSON returns false
        /// </summary>
        [TestMethod]
        public void Missing_Search_Parameters_Returns_False()
        {
            JSONData data = new JSONData();
            var error = data.CreateData("..\\..\\JSONTestFiles\\NoDateTest.json");
            Assert.AreEqual(error, false);
        }

        /// <summary>
        /// No campsite list in JSON returns false
        /// </summary>
        [TestMethod]
        public void Missing_Campsite_List_Returns_False()
        {
            JSONData data = new JSONData();
            var error = data.CreateData("..\\..\\JSONTestFiles\\NoCampsites.json");
            Assert.AreEqual(error, false);
        }

        /// <summary>
        /// No reservation list in JSON returns false
        /// </summary>
        [TestMethod]
        public void Missing_Reservation_List_Returns_False()
        {
            JSONData data = new JSONData();
            var error = data.CreateData("..\\..\\JSONTestFiles\\NoReservations.json");
            Assert.AreEqual(error, false);
        }

        /// <summary>
        /// A completely blank file returns false
        /// </summary>
        [TestMethod]
        public void Blank_File_Returns_False()
        {
            JSONData data = new JSONData();
            var error = data.CreateData("..\\..\\JSONTestFiles\\Blank.json");
            Assert.AreEqual(error, false);
        }

        /// <summary>
        /// A correctly formatted file returns true
        /// </summary>
        [TestMethod]
        public void Expected_Format_Returns_True()
        {
            JSONData data = new JSONData();
            var error = data.CreateData("..\\..\\JSONTestFiles\\Ringer.json");
            Assert.AreEqual(error, true);
        }

    }
}
