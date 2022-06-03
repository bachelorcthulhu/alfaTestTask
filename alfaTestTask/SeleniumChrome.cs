using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using static alfaTestTask.ReadExcel;

namespace alfaTestTask
{
    internal class SeleniumChrome
    {
        static public void RPAchallengeChrome(ExcelFile RPAtable)
        {

            string[] tagNames = new string[] { "FirstName", "LastName", "CompanyName", "Role", "Address", "Email", "Phone" };
            for (int i = 0; i < tagNames.Length; i++)
            {
                RPAtable.DataFromExcelFile[i][0] = tagNames[i];
            }

            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://rpachallenge.com/");
            IList<IWebElement> buttons = driver.FindElements(By.TagName("button"));




            foreach (IWebElement e in buttons)
            {
                System.Console.WriteLine(e.GetAttribute("outerHTML"));

                if (e.GetAttribute("outerHTML").Contains("Start"))
                {
                    e.Click();
                }
            }

            int quantityOfRounds = 10;

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
