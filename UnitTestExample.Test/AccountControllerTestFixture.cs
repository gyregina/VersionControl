﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)

        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidateEmail(email);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        //"A jelszó legalább 8 karakter hosszú kell legyen, 
        //csak az angol ABC betűiből és számokból állhat, 
        //és tartalmaznia kell legalább egy kisbetűt, egy nagybetűt és egy számot.");
        [
            Test,
            TestCase("abcdABCD", false),
            TestCase("ABCDEFGH4321", false),
            TestCase("abcd5432", false),
            TestCase("aB1", false),
            TestCase("Ab3Cd455", true)

        ]

        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidatePassword(password);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }


}
