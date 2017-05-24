using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;


namespace ParserFB
{
    public partial class Form1 : Form
    {

        IWebDriver Browser;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Manage().Window.Maximize();
            BoxLog.Text += "Перешли на Facebook";
            Browser.Navigate().GoToUrl("https://facebook.com");

            string URL = Browser.Url;

            IWebElement Name = Browser.FindElement( By.Id("email"));
            IWebElement Password = Browser.FindElement( By.Id("pass"));
            IWebElement Button = Browser.FindElement(By.Id("loginbutton"));
         
            //Вводим данные
            BoxLog.Text += "\nВводим логин аккаунта";
            Name.SendKeys(AccountLogin.Text);
            System.Threading.Thread.Sleep(3000);
            BoxLog.Text += "\nВводим пароль аккаунта";
            Password.SendKeys(AccountPassword.Text);
            System.Threading.Thread.Sleep(3000);
            BoxLog.Text += "\nКликаем на кнопку Войти";
            Button.Click();
            System.Threading.Thread.Sleep(3000);

            //Вводим поисковый запрос
            System.Threading.Thread.Sleep(3000);
            BoxLog.Text += "\nВводим поисковый запрос";
            IWebElement QueryField = Browser.FindElement(By.XPath("//input[@placeholder='Find friends']"));
            QueryField.SendKeys(Query.Text);

            //Жмем на "найти"
            System.Threading.Thread.Sleep(3000);
            BoxLog.Text += "\nКликаем на кнопку Найти";
            IWebElement SearchButton = Browser.FindElement(By.XPath("//button[@data-testid='facebar_search_button']"));
            SearchButton.Click();

            //Переходим к списку групп
            System.Threading.Thread.Sleep(3000);
            BoxLog.Text += "\nПереходим в группы";
            IWebElement GroupsList = Browser.FindElement(By.XPath("//a[contains(@href,'/search/groups/')]"));
            //GroupsList.Click();
            GroupsList.SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Return);


        }

        IWebElement GetElement(By locator)
        {
            List<IWebElement> elements = Browser.FindElements(locator).ToList();
            if (elements.Count > 0) return elements[0];
            else return null;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Скроллим
            BoxLog.Text += "\nСкроллим";
            IJavaScriptExecutor js = Browser as IJavaScriptExecutor;
            System.Threading.Thread.Sleep(5000);

            for (int i = 0; i < 600; i++)
            {
                IWebElement AllResultDiv = GetElement(By.XPath("//div[@id='browse_end_of_results_footer']"));
                if (AllResultDiv == null)
                {
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<IWebElement> countgroups = Browser.FindElements(By.XPath("//a[contains(@data-testid,'serp_result_link')]")).ToList();
            
            for (int cg = 0; cg < countgroups.Count; cg++)
            {
                groupslistbox.Text += countgroups[cg].GetAttribute("href")+ "\n";
            }

        }
    }
}
