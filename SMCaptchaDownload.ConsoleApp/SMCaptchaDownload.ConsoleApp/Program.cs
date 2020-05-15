using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Chrome;
using Tiny.RestClient;

namespace SMCaptchaDownload.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                // Sürücü ve ayar
                ChromeOptions options = new ChromeOptions();
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
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(
                    @"C:\Users\omnyvz\Desktop\test.png",
                    ScreenshotImageFormat.Png);

                // Ekran kaydından captcha bölümünü kes
                Image img = Image.FromFile(@"C:\Users\omnyvz\Desktop\test.png");
                Bitmap bmpImage = new Bitmap(img);
                Bitmap bmpCrop = bmpImage.Clone(
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
