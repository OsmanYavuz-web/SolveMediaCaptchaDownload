// Osman Yavuz
// omnyvz.yazilim@gmail.com
// 
// SMCaptchaDownload.ConsoleApp - Program.cs
// Creation Date: 15.05.2020 23:49

using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SMCaptchaDownload.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // Sürücü ve ayar
                var options = new ChromeOptions();
                options.AddArgument("--window-size=800,600");
                IWebDriver driver = new ChromeDriver(options);

                // Siteye Git
                driver.Navigate().GoToUrl("https://tensorcrypto.website/etc/");

                // Captcha'ya odaklanma
                driver.FindElement(By.XPath("//input[@name='adcopy_response']"))
                    .SendKeys("");

                // Bekle
                Thread.Sleep(2000);


                // Ekran kaydı al
                var image = ((ITakesScreenshot) driver).GetScreenshot();
                image.SaveAsFile(
                    @"C:\Users\omnyvz\Desktop\test.png",
                    ScreenshotImageFormat.Png);

                // Ekran kaydından captcha bölümünü kes
                var img = Image.FromFile(@"C:\Users\omnyvz\Desktop\test.png");
                var bmpImage = new Bitmap(img);
                var bmpCrop = bmpImage.Clone(
                    new Rectangle(
                        235, 57, // captcha konumu
                        300, 150 // captcha boyutu
                    ), bmpImage.PixelFormat);

                // Captcha 
                bmpCrop.Save(@"C:\Users\omnyvz\Desktop\captcha.jpg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}