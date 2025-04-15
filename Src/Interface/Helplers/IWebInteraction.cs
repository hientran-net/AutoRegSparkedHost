using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Src.Interface.Helplers;

public interface IWebInteraction
{
    public static void SmoothScrollToElement(IWebDriver driver, IWebElement element){}
    public static void SmoothClickElement(IWebDriver driver, IWebElement element){}
    public static void SmoothInputText(IWebDriver driver, IWebElement element, string text){}
    
}