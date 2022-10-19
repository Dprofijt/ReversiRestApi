using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ReversieISpelImplementatie.Model;
using ReversiRestApi;
using ReversiRestApi.Controllers;
using Assert = NUnit.Framework.Assert;

namespace NUnitTestProjectReversiSpel
{
    public class SpelControllerTest
    {

        // Make tests for SpelController
        [Test]
        public void GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            // Arrange
            SpelRepository spelRepository = new SpelRepository();
            SpelController spelController = new SpelController(spelRepository);

            // Act
            var result = spelController.GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            var okObject = result.Result as OkObjectResult;

            Assert.IsNotNull(okObject.Value);
            var list = okObject.Value as IEnumerable<string>;

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count() > 0);




            // Arrange
            //SpelRepository spelRepository = new SpelRepository();
            //SpelController spellController = new SpelController(spelRepository);



            // Act
            //Spel spel = new Spel();
            //spel.Omschrijving = "Test";
            //spel.Speler1Token = "TestSpeler";
            //spelRepository.AddSpel(spel);

            //Spel spel2 = new Spel();
            //spel2.Omschrijving = "Test2";
            //spel2.Speler1Token = "TestSpeler2";
            //spelRepository.AddSpel(spel2);

            //// Assert
            //// Get the spels and check if the description is correct
            //var result = spellController.GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler();
            //if (result == null)
            //{
            //    Assert.Fail();
            //}
            //foreach (var spelOmschrijving in result)
            //{
            //    Assert.IsTrue(spelOmschrijving == "Test" || spelOmschrijving == "Test2");
            //}


        }
        [Test]
        public void GetSpelMetSpelToken()
        {
            // Arrange
            SpelRepository spelRepository = new SpelRepository();
            SpelController spellController = new SpelController(spelRepository);

            // Act
            Spel spel = new Spel(){Token = "TokenTest"};

            spelRepository.AddSpel(spel);


            // Assert
            // Get the spels and check if the description is correct
            var result = spellController.GetSpelMetSpelToken("TokenTest");
            var expect = spel;

            Assert.AreEqual(expect, result);
        }


    }
}
