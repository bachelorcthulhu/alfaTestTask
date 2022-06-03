using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using static alfaTestTask.ReadExcel;

namespace alfaTestTask
{
    internal class SeleniumFirefox
    {
        static public void RPAchallengeFirefox(ExcelFile RPAtable)
        {
            //We will search essentially fields with HTML code. For that we will check "outerHTML" on subject of contains essentially tags
            //tagNames it's array with tags of input fields, that we will use 
            //We will verify input field next way: 
            //1. Check, if "outerHTML" contains first tagName in array - it will be always first element in jagged array - [i][0], where i from 0 to N
            //2. We check all first elements, in order to find necessary
            //3. If we don't find any inclusion, that means that this field is "Sumbit" field - next round button
            string[] tagNames = new string[] { "FirstName", "LastName", "CompanyName", "Role", "Address", "Email", "Phone" };
            for (int i = 0; i < tagNames.Length; i++)
            {
                RPAtable.DataFromExcelFile[i][0] = tagNames[i];
            }
            //We activate driver of our browser and going to https://rpachallenge.com
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var options = new FirefoxOptions();
            var driver = new FirefoxDriver(options);
            driver.Navigate().GoToUrl("https://rpachallenge.com/");
            //initializing list with buttons - to find Start button and start challenge 
            IList<IWebElement> buttons = driver.FindElements(By.TagName("button"));

            foreach (IWebElement e in buttons)
            {
                System.Console.WriteLine(e.GetAttribute("outerHTML"));

                if (e.GetAttribute("outerHTML").Contains("Start"))
                {
                    e.Click();
                }
            }
            //It's variable - count of rounds on rpachallenge.com - there are 10 rounds;
            int quantityOfRounds = 10;
            //Now we use strategy, described above - we will 10 times verifies all input buttons
            //Besides next round button - we will not interact with that button until next turn 
            for (int i = 0; i < quantityOfRounds; i++)
            {
                IList<IWebElement> elements = driver.FindElements(By.TagName("input"));
                foreach (IWebElement e in elements)
                {
                    for (int j = 0; j < RPAtable.DataFromExcelFile.Length; j++)
                    {
                        if (e.GetAttribute("outerHTML").Contains(RPAtable.DataFromExcelFile[j][0]))
                        {
                            e.SendKeys(RPAtable.DataFromExcelFile[j][i + 1]);
                            break;
                        }
                    }
                }
                // And now we searching for "Sumbit" (next round) button. "btn uiColorButton" - outerHTML of "Sumbut" button, that we search
                foreach (IWebElement e in elements)
                {
                    if (e.GetAttribute("outerHTML").Contains("btn uiColorButton"))
                    {
                        e.Click();
                    }
                }
            }
        }
    }
}
