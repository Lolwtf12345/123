using robotManager.Helpful;
using System.Threading;
using robotManager.Products;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.ComponentModel;
using System.Text;
using robotManager;
using System.Threading.Tasks;
using wManager.Wow.Bot.States;
using robotManager.FiniteStateMachine;
using robotManager.Events;
using wManager.Events;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Windows.Forms;
using wManager.Wow;

public class Main : wManager.Plugin.IPlugin
{
	//3
    private bool _isLaunched;
    public void Initialize()
    {
        LastCaptchaTime = DateTime.Now;
        while (!Conditions.InGameAndConnected && Products.IsStarted)
        {
            Logging.Write("[Captcha] Started in Logount Thread.Sleep()");
            Thread.Sleep(10000);
        }
        _isLaunched = true;
        Logging.Write("[Captcha] Started");
        var CurrentPageList = new List<string>();
        //robotManager.Helpful.Var.SetVar("dbgOutput", n);
        //int i = 1;
        EventsLuaWithArgs.OnEventsLuaWithArgs += CaptchaEventsChecker;
        while (Products.IsStarted && _isLaunched)
        {
            if (Conditions.InGameAndConnected)
            {
                CheckCaptcha();
            }
            Thread.Sleep(1000);
            //i = i + 1;

        }
    }

    public void Dispose()
    {
        Logging.Write("[Captcha] Disposed");
        EventsLuaWithArgs.OnEventsLuaWithArgs -= CaptchaEventsChecker;
        _isLaunched = false;
        TaskStarted = false;
        // Code to run when the bot is stopped
    }

    public void Settings()
    {
        CopyLogCaptchaFailed();
        //ScreenShootCaptcha("CaptchaFailed");
        /*Lua.RunMacroText(@"/run SendSystemMessage(""_8___88888______88_____888____"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""888_88___88____888____88______"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""_88_88___88__88__8___8888888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""_88_88___88_88888888_88___88__"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""888__88888_____888____88888___"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_____________________________"")"); //6
        CapchaLines.Clear();
        CapchaLines.Add("_8___88888______88____8__");
        CapchaLines.Add("888_88___88____888___888_");
        CapchaLines.Add("_88_88___88__88__8____88_");
        CapchaLines.Add("_88_88___88_88888888__88_");
        CapchaLines.Add("888__88888_____888___888_");
        CheckDigit(CapchaLines);*/
        //int maxlength = DigitMaxLengthColon[0][1];
        //Logging.Write("Макс число " + maxlength);
        // ScreenShootCaptcha("CaptchaFailed");
        //ScreenShootCaptcha("CaptchaFailed");
        //Capcha4test();
        //_settings.ToForm();
        //_settings.Save();
        // Code to run when someone clicks the plugin settings button
    }
    private void LOG(string msg)
    {
        Logging.Write("[Roboalert log]: " + msg + "", Logging.LogType.Normal, System.Drawing.Color.DarkGreen);
    }
    public class number
    {
        /*        public number()
                {
                    digit = "";
                    russian = "";
                    english = "";
                }*/
        public string digit;
        public string english;
        public string russian;
        public static List<number> DicNumbers = new List<number>
        {
            new number {digit = "0", russian = "ноль", english = "zero"},
            new number {digit = "1", russian = "один", english = "one"},
            new number {digit = "2", russian = "два", english = "two"},
            new number {digit = "3", russian = "три", english = "three"},
            new number {digit = "4", russian = "четыре", english = "four"},
            new number {digit = "5", russian = "пять", english = "five"},
            new number {digit = "6", russian = "шесть", english = "six"},
            new number {digit = "7", russian = "семь", english = "seven"},
            new number {digit = "8", russian = "восемь", english = "eigth"},
            new number {digit = "9", russian = "девять", english = "nine"},
       new number {digit ="10", russian = "десять", english = "ten"},
       new number {digit ="11", russian = "одиннадцать", english = "eleven"},
       new number {digit ="12", russian = "двенадцать", english = "twelve"},
       new number {digit ="13", russian = "тринадцать", english = "thirteen"},
       new number {digit ="14", russian = "четырнадцать", english = "fourteen"},
       new number {digit ="15", russian = "пятнадцать", english = "fifteen"},
       new number {digit ="16", russian = "шестнадцать", english = "sixteen"},
       new number {digit ="17", russian = "семнадцать", english = "seventeen"},
       new number {digit ="18", russian = "восемнадцать", english = "eighteen"},
       new number {digit ="19", russian = "девятнадцать", english = "nineteen"},
       new number {digit ="20", russian = "двадцать", english = "twenty"},
       new number {digit ="21", russian = "двадцать один", english = "twenty-one"},
       new number {digit ="22", russian = "двадцать два", english = "twenty-two"},
       new number {digit ="23", russian = "двадцать три", english = "twenty-three"},
       new number {digit ="24", russian = "двадцать четыре", english = "twenty-four"},
       new number {digit ="25", russian = "двадцать пять", english = "twenty-five"},
       new number {digit ="26", russian = "двадцать шесть", english = "twenty-six"},
       new number {digit ="27", russian = "двадцать семь", english = "twenty-seven"},
       new number {digit ="28", russian = "двадцать восемь", english = "twenty-eight"},
       new number {digit ="29", russian = "двадцать девять", english = "twenty-nine"},
       new number {digit ="30", russian = "тридцать", english = "thirty"},
       new number {digit ="31", russian = "тридцать один", english = "thirty-one"},
       new number {digit ="32", russian = "трицать два", english = "thirty-two"},
       new number {digit ="33", russian = "тридцать три", english = "thirty-three"},
       new number {digit ="34", russian = "тридцать четыре", english = "thirty-four"},
       new number {digit ="35", russian = "тридцать пять", english = "thirty-five"},
       new number {digit ="36", russian = "тридцать шесть", english = "thirty-six"},
       new number {digit ="37", russian = "тридцать семь", english = "thirty-seven"},
       new number {digit ="38", russian = "тридцать восемь", english = "thirty-eight"},
       new number {digit ="39", russian = "тридцать девять", english = "thirty-nine"},
       new number {digit ="40", russian = "сорок", english = "forty"},
       new number {digit ="41", russian = "сорок один", english = "forty-one"},
       new number {digit ="42", russian = "сорок два", english = "forty-two"},
       new number {digit ="43", russian = "сорок три", english = "forty-three"},
       new number {digit ="44", russian = "сорок четыре", english = "forty-four"},
       new number {digit ="45", russian = "сорок пять", english = "forty-five"},
       new number {digit ="46", russian = "сорок шесть", english = "forty-six"},
       new number {digit ="47", russian = "сорок семь", english = "forty-seven"},
       new number {digit ="48", russian = "сорок восемь", english = "forty-eight"},
       new number {digit ="49", russian = "сорок девять", english = "forty-nine"},
       new number {digit ="50", russian = "пятьдесят", english = "fifty"},
       new number {digit ="51", russian = "пятьдесят один", english = "fifty-one"},
       new number {digit ="52", russian = "пятьдесят два", english = "fifty-two"},
       new number {digit ="53", russian = "пятьдесят три", english = "fifty-three"},
       new number {digit ="54", russian = "пятьдесят четыре", english = "fifty-four"},
       new number {digit ="55", russian = "пятьдесят пять", english = "fifty-five"},
       new number {digit ="56", russian = "пятьдесят шесть", english = "fifty-six"},
       new number {digit ="57", russian = "пятьдесят семь", english = "fifty-seven"},
       new number {digit ="58", russian = "пятьдесят восемь", english = "fifty-eight"},
       new number {digit ="59", russian = "пятьдесят девять", english = "fifty-nine"},
       new number {digit ="60", russian = "шестьдесят", english = "sixty"},
       new number {digit ="61", russian = "шестьдесят один", english = "sixty-one"},
       new number {digit ="62", russian = "шестьдесят два", english = "sixty-two"},
       new number {digit ="63", russian = "шестьдесят три", english = "sixty-three"},
       new number {digit ="64", russian = "шестьдесят четыре", english = "sixty-four"},
       new number {digit ="65", russian = "шестьдесят пять", english = "sixty-five"},
       new number {digit ="66", russian = "шестьдесят шесть", english = "sixty-six"},
       new number {digit ="67", russian = "шестьдесят семь", english = "sixty-seven"},
       new number {digit ="68", russian = "шестьдесят восемь", english = "sixty-eight"},
       new number {digit ="69", russian = "шестьдесят девять", english = "sixty-nine"},
       new number {digit ="70", russian = "семьдесят", english = "seventy"},
       new number {digit ="71", russian = "семьдесят один", english = "seventy-one"},
       new number {digit ="72", russian = "семьдесят два", english = "seventy-two"},
       new number {digit ="73", russian = "семьдесят три", english = "seventy-three"},
       new number {digit ="74", russian = "семьдесят четыре", english = "seventy-four"},
       new number {digit ="75", russian = "семьдесят пять", english = "seventy-five"},
       new number {digit ="76", russian = "семьдесят шесть", english = "seventy-six"},
       new number {digit ="77", russian = "семьдесят семь", english = "seventy-seven"},
       new number {digit ="78", russian = "семьдесят восемь", english = "seventy-eight"},
       new number {digit ="79", russian = "семьдесят девять", english = "seventy-nine"},
       new number {digit ="80", russian = "восемьдесят", english = "eighty"},
       new number {digit ="81", russian = "восемьдесят один", english = "eighty-one"},
       new number {digit ="82", russian = "восемьдесят два", english = "eighty-two"},
       new number {digit ="83", russian = "восемьдесят три", english = "eighty-three"},
       new number {digit ="84", russian = "восемьдесят четыре", english = "eighty-four"},
       new number {digit ="85", russian = "восемьдесят пять", english = "eighty-five"},
       new number {digit ="86", russian = "восемьдесят шесть", english = "eighty-six"},
       new number {digit ="87", russian = "восемьдесят семь", english = "eighty-seven"},
       new number {digit ="88", russian = "восемьдесят восемь", english = "eighty-eight"},
       new number {digit ="89", russian = "восемьдесят девять", english = "eighty-nine"},
       new number {digit ="90", russian = "девяносто", english = "ninety"},
       new number {digit ="91", russian = "девяносто один", english = "ninety-one"},
       new number {digit ="92", russian = "девяносто два", english = "ninety-two"},
       new number {digit ="93", russian = "девяносто три", english = "ninety-three"},
       new number {digit ="94", russian = "девяносто четыре", english = "ninety-four"},
       new number {digit ="95", russian = "девяносто пять", english = "ninety-five"},
       new number {digit ="96", russian = "девяносто шесть", english = "ninety-six"},
       new number {digit ="97", russian = "девяносто семь", english = "ninety-seven"},
       new number {digit ="98", russian = "девяносто восемь", english = "ninety-eigth"},
       new number {digit ="99", russian = "девяносто девять", english = "ninety-nine"},
       new number {digit ="100", russian ="сто", english = "hundred"},
        };
    }

    /*private Dictionary<string, string> DicNumbers = new Dictionary<string, string>()
    {
        {"0", "ноль"},
        {"1", "один"},
        {"2", "два"},
        {"3", "три"},
        {"4", "четыре"},
        {"5", "пять"},
        {"6", "шесть"},
        {"7", "семь"},
        {"8", "восемь"},
        {"9", "девять"},
        {"10", "десять"},
        {"11", "одиннадцать"},
        {"12", "двенадцать"},
        {"13", "тринадцать"},
        {"14", "четырнадцать"},
        {"15", "пятнадцать"},
        {"16", "шестнадцать"},
        {"17", "семнадцать"},
        {"18", "восемнадцать"},
        {"19", "девятнадцать"},
        {"20", "двадцать"},
        {"21", "двадцать один"},
        {"22", "двадцать два"},
        {"23", "двадцать три"},
        {"24", "двадцать четыре"},
        {"25", "двадцать пять"},
        {"26", "двадцать шесть"},
        {"27", "двадцать семь"},
        {"28", "двадцать восемь"},
        {"29", "двадцать девять"},
        {"30", "тридцать"},
        {"31", "тридцать один"},
        {"32", "трицать два"},
        {"33", "тридцать три"},
        {"34", "тридцать четыре"},
        {"35", "тридцать пять"},
        {"36", "тридцать шесть"},
        {"37", "тридцать семь"},
        {"38", "тридцать восемь"},
        {"39", "тридцать девять"},
        {"40", "сорок"},
        {"41", "сорок один"},
        {"42", "сорок два"},
        {"43", "сорок три"},
        {"44", "сорок четыре"},
        {"45", "сорок пять"},
        {"46", "сорок шесть"},
        {"47", "сорок семь"},
        {"48", "сорок восемь"},
        {"49", "сорок девять"},
        {"50", "пятьдесят"},
        {"51", "пятьдесят один"},
        {"52", "пятьдесят два"},
        {"53", "пятьдесят три"},
        {"54", "пятьдесят четыре"},
        {"55", "пятьдесят пять"},
        {"56", "пятьдесят шесть"},
        {"57", "пятьдесят семь"},
        {"58", "пятьдесят восемь"},
        {"59", "пятьдесят девять"},
        {"60", "шестьдесят"},
        {"61", "шестьдесят один"},
        {"62", "шестьдесят два"},
        {"63", "шестьдесят три"},
        {"64", "шестьдесят четыре"},
        {"65", "шестьдесят пять"},
        {"66", "шестьдесят шесть"},
        {"67", "шестьдесят семь"},
        {"68", "шестьдесят восемь"},
        {"69", "шестьдесят девять"},
        {"70", "семьдесят"},
        {"71", "семьдесят один"},
        {"72", "семьдесят два"},
        {"73", "семьдесят три"},
        {"74", "семьдесят четыре"},
        {"75", "семьдесят пять"},
        {"76", "семьдесят шесть"},
        {"77", "семьдесят семь"},
        {"78", "семьдесят восемь"},
        {"79", "семьдесят девять"},
        {"80", "восемьдесят"},
        {"81", "восемьдесят один"},
        {"82", "восемьдесят два"},
        {"83", "восемьдесят три"},
        {"84", "восемьдесят четыре"},
        {"85", "восемьдесят пять"},
        {"86", "восемьдесят шесть"},
        {"87", "восемьдесят семь"},
        {"88", "восемьдесят восемь"},
        {"89", "восемьдесят девять"},
        {"90", "девяносто"},
        {"91", "девяносто один"},
        {"92", "девяносто два"},
        {"93", "девяносто три"},
        {"94", "девяносто четыре"},
        {"95", "девяносто пять"},
        {"96", "девяносто шесть"},
        {"97", "девяносто семь"},
        {"98", "девяносто восемь"},
        {"99", "девяносто девять"},
        {"100", "сто"},
    };*/
    public class Numeral
    {
        public string digit;
        public string russian;
        public string english;
        public static List<Numeral> NumbersVariantsInTextAdjective = new List<Numeral>
    {
        new Numeral{digit = "1",russian =  "первую", english = "first"},
        new Numeral{digit = "2",russian =  "вторую", english = "second"},
        new Numeral { digit = "3",russian =  "третью", english = "third"},
        new Numeral { digit = "4",russian =  "четвертую", english = "fourth"},
        new Numeral { digit = "5",russian =  "пятую", english = "fifth"},
        new Numeral { digit = "6",russian =  "шестую", english = "sixth"},
        new Numeral { digit = "7",russian =  "седьмую", english = "seventh"},
        new Numeral { digit = "8",russian =  "восьмую", english = "eighth"},
        new Numeral { digit = "9",russian =  "девятую", english = "ninth"},
        new Numeral { digit = "10",russian =  "десятую", english = "tenth"},
        new Numeral { digit = "11",russian =  "одиннадцатую", english = "eleventh"},
        new Numeral { digit = "12",russian =  "двенадцатую", english = "twelfth"},
        new Numeral { digit = "13",russian =  "тринадцатую", english = "thirteenth"},
        new Numeral { digit = "14",russian =  "четырнадцатую", english = "fourteenth"},
        new Numeral { digit = "15",russian =  "пятнадцатую", english = "fifteenth"},
        new Numeral { digit = "16",russian =  "шестнадцатую", english = "sixteenth"},
        new Numeral { digit = "17",russian =  "семнадцатую", english = "seventeenth"},
        new Numeral { digit = "18",russian =  "восемнадцатую", english = "eighteenth"},
        new Numeral { digit = "19",russian =  "девятнадцатую", english = "nineteenth"},
        new Numeral { digit = "20", russian = "двадцатую", english = "twentieth"},
        new Numeral { russian =  "последнюю",  english = "last"},
    };
    }
    public string GossipText
    {
        get
        {
            //if (Lua.LuaDoString<string>("return GossipGreetingText:GetText()") != "Filler Text")
            //{
            string oldString = Lua.LuaDoString<string>("return GossipGreetingText:GetText()").ToLower();
            System.Text.StringBuilder newString = new System.Text.StringBuilder();
            foreach (var ch in oldString)
                if (!Char.IsControl(ch))
                    newString.Append(ch);
            string text = newString.ToString();
            //var text = Lua.LuaDoString<string>("return GossipGreetingText:GetText()").ToLower().Replace("\n", " ");
            //if (text.Contains(ObjectManager.Me.Name.ToLower() + ", our security system has detected suspicious activity on your character. please solve a simple puzzle to eliminate sanctions for your character."))
            //text = text.Remove(0, text.IndexOf("character."));
            //text = System.Text.RegularExpressions.Regex.Replace(text, @"\n", " ");
            //text.Replace(@"\n", " ");
            return text;
            //}

            //else
            //return "";
        }
    }
   /* public string StepName()
    {
        string currentstep = null;
        var p = Quest.QuesterCurrentContext.Profile as Quester.Profile.QuesterProfile;
        if (p != null)
        {
            if (p.QuestsSorted[Quest.QuesterCurrentContext.CurrentStep].Action == wManager.Wow.Class.QuestAction.Pulse && p.QuestsSorted[Quest.QuesterCurrentContext.CurrentStep].NameClass != null)
            {
                currentstep = p.QuestsSorted[Quest.QuesterCurrentContext.CurrentStep].NameClass;
            }
        }
        else
        {
            currentstep = null;
        }
        //Logging.Write(currentstep);
        return currentstep;
    }*/
    public string GossipTitleButtonText(int number)
    {
        return Lua.LuaDoString<string>(String.Format("return GossipTitleButton{0}:GetText()", number));
    }
    public void CloseGossip()
    {
        if (Lua.LuaDoString<bool>("if GossipFrame then if GossipFrame:IsVisible() then return true end end"))
            Lua.LuaDoString("GossipFrameCloseButton:Click()");
    }
    public int ButtonCount
    {
        get
        {
            var count = 0;
            for (int i = 1; i <= 10; i++)
            {
                if (Lua.LuaDoString<string>(String.Format("return GossipTitleButton{0}:GetText()", i)) != "")
                {
                    count++;
                }
            }
            
            return count;
        }
    }
    private string FormatLua(string str, params object[] names)
    {
        return string.Format(str, names.Select(s => s.ToString().Replace("'", "\\'").Replace("\"", "\\\"")).ToArray());
    }

    private void print(string msg)
    {
        Lua.LuaDoString(FormatLua(@"DEFAULT_CHAT_FRAME:AddMessage(""{0}"")", "|cff777777[" + Time() + "] |cff777777[i]: |cff00ffaa" + msg + ""));
    }
    private string Time()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }
    public static DateTime LastCaptchaTime;
   /* void gossiptest()
    {
        var GossipText = @"moltenshield, our security system has detected suspicious activity on your character. please solve a simple puzzle to eliminate sanctions for your character.
 
 select the first digit in the number 100406";

        char[] denied = new[] { ' ', '\n', '\t', '\r' };
        System.Text.StringBuilder newString = new System.Text.StringBuilder();
        string oldString = GossipText;
        foreach (var ch in oldString)
            if (!Char.IsWhiteSpace(ch) && !Char.IsControl(ch))
                newString.Append(ch);
        Logging.Write("GossipText = " + newString);
        //Console.WriteLine(newString.ToString());
        //GossipText = GossipText.Replace(@"\n", "");
        //Logging.Write("GossipText = " + GossipText);
    }*/
    /*void Captchadevtools()
    {
        string currentstep = null;
*//*        var p = Quest.QuesterCurrentContext.Profile as Quester.Profile.QuesterProfile;
        if (p != null)
        {
            if (p.QuestsSorted[Quest.QuesterCurrentContext.CurrentStep].Action == wManager.Wow.Class.QuestAction.Pulse && p.QuestsSorted[Quest.QuesterCurrentContext.CurrentStep].NameClass != null)
            {
                currentstep = p.QuestsSorted[Quest.QuesterCurrentContext.CurrentStep].NameClass;
            }
        }
        else
        {
            currentstep = null;
        }*//*
        var GossipText = Lua.LuaDoString<string>("return GossipGreetingText:GetText()").ToLower().Replace(@"\d", "");
        Lua.LuaDoString("print('" + GossipText + "')");








        Logging.Write("GossipFrame:IsVisible() = " + Lua.LuaDoString<bool>("if GossipFrame:IsVisible() then return true end"));
        Logging.Write("GossipText = " + GossipText);
        Logging.Write("GossipText != Filler Text = " + (GossipText != "Filler Text"));
        Logging.Write("!GossipText.Contains(greetings) = " + !GossipText.Contains("greetings"));
        Logging.Write("!GossipText.Contains(the vrykul rests) = " + !GossipText.Contains("the vrykul rests"));
        Logging.Write("!String.IsNullOrEmpty(GossipText) = " + !System.String.IsNullOrEmpty(GossipText));
        Logging.Write("Lua.LuaDoString<string>(return GossipFrameNpcNameText:GetText()).ToLower() == ObjectManager.Me.Name.ToLower() = " + (Lua.LuaDoString<string>("return GossipFrameNpcNameText:GetText()").ToLower() == ObjectManager.Me.Name.ToLower()));
        Logging.Write("GossipText != Filler Text = " + (GossipText != "Filler Text"));
        Logging.Write("!wManager.Wow.Bot.States.ToTown.GoToTownInProgress = " + (!wManager.Wow.Bot.States.ToTown.GoToTownInProgress));
        Logging.Write("!StepName().ToLower().Contains(sell) = " + !currentstep.ToLower().Contains("sell"));
        string Button1text = !Lua.LuaDoString<bool>("return GossipTitleButton1:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton1:GetText()")) ? "" : " GossipTitleButton1:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton1:GetText()");
        string Button2text = !Lua.LuaDoString<bool>("return GossipTitleButton2:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton2:GetText()")) ? "" : " GossipTitleButton2:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton2:GetText()");
        string Button3text = !Lua.LuaDoString<bool>("return GossipTitleButton3:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton3:GetText()")) ? "" : " GossipTitleButton3:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton3:GetText()");
        string Button4text = !Lua.LuaDoString<bool>("return GossipTitleButton4:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton4:GetText()")) ? "" : " GossipTitleButton4:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton4:GetText()");
        string Button5text = !Lua.LuaDoString<bool>("return GossipTitleButton5:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton5:GetText()")) ? "" : " GossipTitleButton5:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton5:GetText()");
        string Button6text = !Lua.LuaDoString<bool>("return GossipTitleButton6:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton6:GetText()")) ? "" : " GossipTitleButton6:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton6:GetText()");
        string Button7text = !Lua.LuaDoString<bool>("return GossipTitleButton7:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton7:GetText()")) ? "" : " GossipTitleButton7:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton7:GetText()");
        string Button8text = !Lua.LuaDoString<bool>("return GossipTitleButton8:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton8:GetText()")) ? "" : " GossipTitleButton8:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton8:GetText()");
        string Button9text = !Lua.LuaDoString<bool>("return GossipTitleButton9:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton9:GetText()")) ? "" : " GossipTitleButton9:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton9:GetText()");
        string Button10text = !Lua.LuaDoString<bool>("return GossipTitleButton10:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton10:GetText()")) ? "" : " GossipTitleButton10:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton10:GetText()");
        var button = new[] { Button1text, Button2text, Button3text, Button4text, Button5text, Button6text, Button7text, Button8text, Button9text, Button10text };
        string allbuttontext = "";
        foreach (var b in button)
        {
            allbuttontext = allbuttontext + b;
        }
        Logging.Write("allbutton text = " + allbuttontext);
    }*/
    private void sendDiscordMessageCaptcha(String contentBody)
    {
        try
        {

            var webReq = WebRequest.Create("https://discord.com/api/webhooks/768399471041445899/QUto-wJhWvMf9R-QUDhQ_u5pLeawU2VXJm-ewO-TDtlpKENZYTuEc7bXfn-t5ybJBvLL") as HttpWebRequest;
            webReq.ContentType = "application/json";
            webReq.Method = "POST";
            using (var streamWriter = new StreamWriter(webReq.GetRequestStream()))
            {
                string json = "{\"content\":\"" + contentBody + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)webReq.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Logging.Write(result);
            }
        }
        catch (Exception e)
        {
            Logging.Write("[RoboAlert] Problem creating Discord Message Notification: " + e);
        }

    }
    private DateTime CaptchaCheck;
    private void PRINT(string msg)
    {
        Lua.LuaDoString("DEFAULT_CHAT_FRAME:AddMessage('[Captcha log]: " + msg + "', 0.2, 1.0, 0.3)");
    }
    private void MaximizeWowWindow()
    {
        if (Display.GetWindowHeight(wManager.Wow.Memory.WowMemory.Memory.WindowHandle) < 600)
        {
            PRINT("MaximizeWowWindow");
            LOG("MaximizeWowWindow");
            Thread.Sleep(10);
            robotManager.Helpful.Win32.Native.ShowWindow(wManager.Wow.Memory.WowMemory.Memory.WindowHandle, 3);
            Thread.Sleep(100);
            for (int l = 0; l < Others.Random(30, 50); l++)
            {
                if (Display.GetWindowHeight(wManager.Wow.Memory.WowMemory.Memory.WindowHandle) < 600)
                    Thread.Sleep(100);
            }
        }
    }
    private void PlaySound(string FileNameOrPath, bool RobotDataFolder = true)
    {
        string path;

        if (RobotDataFolder)
            path = System.Windows.Forms.Application.StartupPath + @"\Data\" + FileNameOrPath + "";
        else
            path = FileNameOrPath;

        if (File.Exists(path))
        {
            Logging.Write("[PlaySound] " + path + "");
            new System.Media.SoundPlayer(path).Play();
        }
    }
    public void testrgx()
    {
        var exampletext = "ykauxexelue, our security system has detected suspicious activity on your character. please solve a simple puzzle to eliminate sanctions for your character.select the number written nоw in the chat.  if you don't see the number, please check if you have enabled the display of system messages in the chat.";
        var IsCaptcha = System.Text.RegularExpressions.Regex.Match(exampletext, @"(S(e|е)L(e|е)(с|c)(т|t) (т|t)(h|н)(e|е) NU(м|m)(b|в)(e|е)R WRI(т|t)(т|t)(e|е)N N(o|о)W IN (т|t)(h|н)(e|е) (с|c)(h|н)(a|а)(т|t))", System.Text.RegularExpressions.RegexOptions.CultureInvariant);
        robotManager.Helpful.Var.SetVar("dbgOutput", IsCaptcha.Success);
    }
    private string ReplaceSimilarLettersRUStoENG(string input)
    {
        return input.ToLower().Replace("e", "е").Replace("a", "а").Replace("т", "t").Replace("у", "y").Replace("о", "o").Replace("р", "p").Replace("н", "h").Replace("к", "k").Replace("х", "x").Replace("с", "c").Replace("в", "b").Replace("м", "m").Replace("ё", "e");
    }
    public bool TaskStarted;
    public List<string> Vseknopki = new List<string>();
    public void CheckCaptcha()
    {

        if (CaptchaCheck < System.DateTime.Now)
        {
            //var GossipText = Lua.LuaDoString<string>("return GossipGreetingText:GetText()").ToLower();
            //Lua.LuaDoString<bool>("if GossipFrame then if GossipFrame:IsVisible() then return true end end")
            //GossipText.Replace("\"", "'");
            //LOG("капчачек");
            
            var IsCaptcha = Regex.Match(GossipText, @"(security system has detected)", RegexOptions.CultureInvariant);
            if ((Lua.LuaDoString<bool>("if GossipFrame:IsVisible() then return true end") || (Lua.LuaDoString<bool>("return GossipTitleButton1:IsVisible()") && !string.IsNullOrEmpty(GossipText))
                /*&& GossipText != "Filler Text"
                && !GossipText.Contains("greet")
                && !GossipText.Contains("welcome")
                && !GossipText.Contains("the vrykul rests")*/
                && IsCaptcha.Success
                //&& !String.IsNullOrEmpty(GossipText)
                //&& Lua.LuaDoString<string>("return GossipFrameNpcNameText:GetText()").ToLower() == ObjectManager.Me.Name.ToLower()
                //&& !StepName().ToLower().Contains("sell")
                && ObjectManager.GetObjectWoWUnit().Count(u => u.IsNpcVendor && u.IsGoodInteractDistance) == 0
                /*&& !wManager.Wow.Bot.States.ToTown.GoToTownInProgress*/)
                )
            {
                //PlaySound("burp.wav");
                Var.SetVar("LastCaptchaTime", DateTime.Now);
                CaptchaCheck = System.DateTime.Now.AddSeconds(Others.Random(10, 20));
                Thread.Sleep(2000);
               
                IsCaptcha = Regex.Match(GossipText, @"(security system has detected)", RegexOptions.CultureInvariant);
                if (/*StepName().ToLower().Contains("sell") ||*/ wManager.Wow.Bot.States.ToTown.GoToTownInProgress || GossipText.Contains("welcome") || GossipText.Contains("greet") || GossipText.Contains("the vrykul rests") || !IsCaptcha.Success)
                    return;
/*                if(wManager.Information.Version != "1.7.2 (33050)")
                {
                    MaximizeWowWindow();
                    robotManager.Helpful.Win32.Native.SetForegroundWindow(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
                }*/
                
                    
               
                
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    if (TaskStarted)
                    {
                        Logging.Write("Поток уже запущен => return");
                        return;
                    }
                    TaskStarted = true;
                    Products.InPause = true;
                    if (ObjectManager.GetObjectWoWGameObject().Count(u => u.IsMailbox && u.GetDistance <= 6) > 0)
                    {
                        
                        if (Var.Exist("MailCount"))
                            Var.SetVar("MailCount", Var.GetVar<int>("MailCount") + 2);
                        Logging.Write("Mailbox is near => MailCount = " + Var.GetVar<int>("MailCount"));
                    }
                    Logging.Write("Начали поток проверки капчи => Products.InPause = true");
                    string Button1text = !Lua.LuaDoString<bool>("return GossipTitleButton1:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton1:GetText()")) ? "" : " GossipTitleButton1:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton1:GetText()");
                    string Button2text = !Lua.LuaDoString<bool>("return GossipTitleButton2:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton2:GetText()")) ? "" : " GossipTitleButton2:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton2:GetText()");
                    string Button3text = !Lua.LuaDoString<bool>("return GossipTitleButton3:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton3:GetText()")) ? "" : " GossipTitleButton3:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton3:GetText()");
                    string Button4text = !Lua.LuaDoString<bool>("return GossipTitleButton4:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton4:GetText()")) ? "" : " GossipTitleButton4:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton4:GetText()");
                    string Button5text = !Lua.LuaDoString<bool>("return GossipTitleButton5:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton5:GetText()")) ? "" : " GossipTitleButton5:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton5:GetText()");
                    string Button6text = !Lua.LuaDoString<bool>("return GossipTitleButton6:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton6:GetText()")) ? "" : " GossipTitleButton6:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton6:GetText()");
                    string Button7text = !Lua.LuaDoString<bool>("return GossipTitleButton7:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton7:GetText()")) ? "" : " GossipTitleButton7:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton7:GetText()");
                    string Button8text = !Lua.LuaDoString<bool>("return GossipTitleButton8:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton8:GetText()")) ? "" : " GossipTitleButton8:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton8:GetText()");
                    string Button9text = !Lua.LuaDoString<bool>("return GossipTitleButton9:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton9:GetText()")) ? "" : " GossipTitleButton9:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton9:GetText()");
                    string Button10text = !Lua.LuaDoString<bool>("return GossipTitleButton10:IsVisible()") || string.IsNullOrEmpty(Lua.LuaDoString<string>("return GossipTitleButton10:GetText()")) ? "" : " GossipTitleButton10:GetText(): " + Lua.LuaDoString<string>("return GossipTitleButton10:GetText()");
                    var button = new[] { Button1text, Button2text, Button3text, Button4text, Button5text, Button6text, Button7text, Button8text, Button9text, Button10text };
                    string allbuttontext = "";
                    Vseknopki.Clear();
                    Logging.Write("Vseknopki.Count = " + Vseknopki.Count);
                    foreach (var b in button)
                    {
                        allbuttontext = allbuttontext + b;
/*                        if(b != "")
                        {
                            Vseknopki.Add(b);
                            LOG("Добавили кнопку " + b + " во все кнопки");
                            print("Добавили кнопку " + b + " во все кнопки");
                        }*/
                    }
                    string textwithoutnewlines = GossipText;
                    if (wManager.Information.Version != "1.7.2 (33050)")
                        sendDiscordMessageCaptcha(System.DateTime.Now + " " + Usefuls.RealmName + " [" + ObjectManager.Me.Name + "] капча с текстом [" + textwithoutnewlines/*GossipText.Replace("\"", "")*/ + "]" + allbuttontext);
                    LOG("капча с текстом " + GossipText + allbuttontext + " в зоне " + Usefuls.MapZoneName + ", " + Usefuls.SubMapZoneName + " " + Usefuls.RealmName + " время " + System.DateTime.Now);
                    print("капча с текстом " + textwithoutnewlines + allbuttontext);
                    LOG("ButtonCount: " + ButtonCount);
                    print("ButtonCount: " + ButtonCount);
                    Thread.Sleep(Others.Random(2000, 3000));
                    Capcha1();
                    Capcha2();
                    Capcha3();
                    Capcha4();
                    Capcha5();
                    Capcha6();
                    Capcha7();
                    Capcha8();
                    Capcha9();
                    Capcha10();
                    Capcha11();
                    Capcha12();
                    Capcha13();
                    Opros();
                    var IsChatCaptcha = Regex.Match(GossipText, @"(S(e|е)L(e|е)(с|c)(т|t) (т|t)(h|н)(e|е) NU(м|m)(b|в)(e|е)R WRI(т|t)(т|t)(e|е)N N(o|о)W IN (т|t)(h|н)(e|е) (с|c)(h|н)(a|а)(т|t))", RegexOptions.IgnoreCase);
                    if (IsChatCaptcha.Success)
                    {
                        Thread.Sleep(Others.Random(5000, 10000));
                    }
                    
                    if(Lua.LuaDoString<bool>("if GossipFrame:IsVisible() then return true end"))
                    {
                        LOG("Капча не пройдена");
                        print("Капча не пройдена");
                        ScreenShootCaptcha("CaptchaFailed");
                        // ScreenShootCaptcha("CaptchaFailed-" + ObjectManager.Me.Name + "-"+ System.DateTime.Now);
                        sendDiscordMessageCaptcha(System.DateTime.Now + " " + Usefuls.RealmName + " [" + ObjectManager.Me.Name + "] капча с текстом [" + textwithoutnewlines/*GossipText.Replace("\"", "")*/ + "]" + allbuttontext + " Не пройдена.");
                    }
                    Logging.Write("Поток проверки закончен => Products.InPause = false");
                    Products.InPause = false;
                    TaskStarted = false;
                });
            }

        }
    }
    public static void ShowWindow()
    {
        if (Display.GetWindowHeight(wManager.Wow.Memory.WowMemory.Memory.WindowHandle) < 600)
        {
            robotManager.Helpful.Win32.Native.ShowWindow(Memory.WowMemory.Memory.WindowHandle, 3);
            Thread.Sleep(1000);
        }
    }
    public static void HideWindow()
    {
        if (Display.GetWindowHeight(wManager.Wow.Memory.WowMemory.Memory.WindowHandle) >= 600)
        {
            robotManager.Helpful.Win32.Native.ShowWindow(Memory.WowMemory.Memory.WindowHandle, 4);
        }
    }
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

    static readonly IntPtr HWND_TOP = new IntPtr(0);

    static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

    const UInt32 SWP_NOSIZE = 0x0001;

    const UInt32 SWP_NOMOVE = 0x0002;

    const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    public static void ScreenShootCaptcha(string description)
    {


        // папка зоны
        //var orderidwithoutletters = System.Text.RegularExpressions.Regex.Replace(order.Id, @"\D", "");
        string subpath = Application.StartupPath + @"\CapchaScreens\";

        // создание папки зоны
        if (!Directory.Exists(subpath))
        {
            Logging.Write("[ScreenShoot]: создание папки для скринов непройденной капчи");
            Directory.CreateDirectory(subpath);
        }
        var name = ObjectManager.Me.Name + "-" + DateTime.Now.ToString("yyyy-MM-dd - HH-mm-ss") + "-" + description + ".jpg";
        var pathandname = subpath + name; // полный путь папка + имя скрина

        if (Conditions.InGameAndConnected)
        {
            ShowWindow();
            //Lua.LuaDoString("DEFAULT_CHAT_FRAME:AddMessage('server info', 0.8, 0.3, 0.0) SendChatMessage('.server info','GUILD',nil,nil)");
            Thread.Sleep(Usefuls.LatencyReal);

            SetWindowPos(wManager.Wow.Memory.WowMemory.Memory.WindowHandle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS); // топмост окно

            //var ScreenshotNameAndPath = "" + DateTime.Now.ToString("yyyy-MM-dd - HH-mm-ss") + " : " + location + " : " + MyFactionLong + " : " + RealmName + "";
            //Lua.LuaDoString(FormatLua("UIErrorsFrame:SetScale(4) UIErrorsFrame:AddMessage('SCREENSHOT +', 1.0, 1.0, 0.0, 53, 1) DEFAULT_CHAT_FRAME:AddMessage('{0}', 0.8, 0.3, 0.0)", ScreenshotNameAndPath));

            if (Conditions.InGameAndConnected)
            {
                Display.ScreenshotWindow(wManager.Wow.Memory.WowMemory.Memory.WindowHandle, pathandname, System.Drawing.Imaging.ImageFormat.Jpeg); // скриним окно
                byte[] bData = File.ReadAllBytes(pathandname);
                UploadToTelegram(bData, LetsGoldBotToken,"photo", CapchaAlertChannel, pathandname, "Captchafailed");
                Logging.Write("[ScreenShoot]: скрин сделан " + pathandname);
                Thread.Sleep(1000);
                HideWindow();
            }
        }
        SetWindowPos(wManager.Wow.Memory.WowMemory.Memory.WindowHandle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS); // не топмост окно
    }
    public static string CapchaAlertChannel = "-1001649493936";
    public static string Lebottoken = "1773461141:AAEr-43S81X5wXuFG43RW2samiklsKsHYIg";
    public static string LetsGoldBotToken = "1649810276:AAHU_XXpEbpTKcUsrlwo7XHvzdH0ByfXveE";
    public static async Task UploadToTelegram(byte[] array, string token, string Filetype, string chatid, string filepath, string Filename)
    {
        //557-556-562-574-557-556-569-569-580-509-509
        /*        var cryptostr = robotManager.Helpful.Others.StringToEncryptString("lolwtf12345");
                robotManager.Helpful.Var.SetVar("dbgOutput", cryptostr);*/
        //System.Windows.Forms.Application.StartupPath
        //var file = System.Windows.Forms.Application.StartupPath + "\\zerkala2.jpg";
        try
        {
            //string url = "https://api.telegram.org/bot" + token + "/sendPhoto?chat_id=" + chatid;
            string url = "https://api.telegram.org/bot" + token + "/send" + Filetype + "?chat_id=" + chatid;
            //string url = "https://api.telegram.org/bot" + LetsGoldBotToken + "/sendDocument?chat_id=" + LetsGoldChannelIid + "&document=";
            //string url = "https://discord.com/api/webhooks/900052776191684658/IU5ySZWAY_XjI-Nnu02In3oZ9_bT9vUWPPgp4v1_wzEshPx7r6PzESobBWLMrjId-T0V";
            using (var client = new System.Net.Http.HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)))
                {
                    //content.Add(new StreamContent(new MemoryStream(image)), "photo", photoname);
                    content.Add(new StreamContent(new MemoryStream(array)), Filetype, Filename);
                    //content.di
                    //content.
                    //
                    using (
                       var message =
                       await client.PostAsync(url, content))
                    //await client.PostAsync("http://www.directupload.net/index.php?mode=upload", content))
                    {
                        var input = await message.Content.ReadAsStringAsync();

                        //return await client.PostAsync(url, content);
                    }
                }
            }
        }
        catch( Exception e)
        {
            Logging.WriteError(e.ToString());
        }
    }
    public List<string> Textcapchicircle = new List<string>
    {
        "select the first digit in the number",
        "select the last digit in the number",
        "select the second digit in the number",
        "select the third digit in the number",
        "select the row with the number",
        "select the largest number",
        "how many books",
        "andrey lives",
        "plates",
        "select the row",
        "on one branch",
        "how much is",
        "how many classes are in world of warcraft",
        "who is the developer",
        "what is the name of the flying city",
        "symbol",
        "уважаемые игроки",
        "dear players",
    };
    void Capcha13()
    {
        if (GossipText.Contains("king of stormwind"))
        {
            LOG("King of Stormwind");
            string otvet = "Varian Wrynn";
            LOG("Ответ: " + otvet);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToLower().Contains(otvet.ToLower()))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }
    
    void Capcha1()
    {
        //Select the LAST digit in the number 29463
        if ((GossipText.Contains("выберите") && GossipText.Contains("в числе")) || (GossipText.Contains("select") && GossipText.Contains("in the number")))
        {
            LOG("капча типа Select the LAST digit in the number");
            print("капча типа Select the LAST digit in the number");
            string cifra = "";
            string chislotext = "";
            /*if (GossipText.Contains("в числе"))
                chislotext = GossipText.Remove(0, GossipText.IndexOf("в числе ") + 8);
            if (GossipText.Contains("in the number"))
                chislotext = GossipText.Substring(GossipText.LastIndexOf('r') + 2);*/
            chislotext = System.Text.RegularExpressions.Regex.Replace(GossipText, @"\D", String.Empty);
            if (GossipText.Contains("последнюю") || GossipText.Contains("last"))
            {
                cifra = chislotext[chislotext.Length - 1].ToString(); //последняя цифра в типе string
            }
            else
            {
                var kvpnomer = Numeral.NumbersVariantsInTextAdjective.FirstOrDefault(v => GossipText.Contains(v.russian) || GossipText.Contains(v.english));//значение(value) из dictionary NumbersVariantsInTextAdjective которое есть в GossipText
                int nomer = System.Int32.Parse(kvpnomer.digit); //ключ от kvpnomer конвертированный в int
                cifra = chislotext[nomer - 1].ToString(); //цифра соответствующая номеру начиная с 0
            }
            //int chislo = System.Int32.Parse(text);

            ///int cifraint = System.Int32.Parse(cifrachar.ToString());
            var DicNumber = number.DicNumbers.FirstOrDefault(v => v.digit == cifra);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i).ToLower() != "" && (GossipTitleButtonText(i).ToLower() == DicNumber.english || GossipTitleButtonText(i).ToLower() == DicNumber.russian || GossipTitleButtonText(i).ToLower() == DicNumber.digit))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }

    }
    void Capcha2()
    {
        //Select the row with the correct solution, 8 + 4 = ?
        //Выберите строку с правильным решением
        //var GossipText = "Select the row with the correct solution, 8 +  4 = ?";

        string pattern = @"\d{1,}\s+\W\s+\d{1,}"; // /d - соответствует любой десятичной цифре {1} - 
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Match match = regex.Match(GossipText);
        //Logging.Write(match.ToString());
        //System.Text.RegularExpressions.MatchCollection matches = regex.Matches(GossipText);
        if (System.Text.RegularExpressions.Regex.IsMatch(GossipText, pattern))/*(GossipText.Contains("выберите") && GossipText.Contains("решением")) || (GossipText.Contains("select") && GossipText.Contains("solution")))*/
        {
            print("Select the row with the correct solution");
            if (GossipText.Contains("solution"))
                LOG("Select the row with the correct solution");
            if (GossipText.Contains("how much"))
                LOG("how much is int + int ?");
            /*var Gossiptext = "Select the row with the correct solution, 8 + 4 = ?";
            string pattern = @"\d{1}\s\W\s\d{1}";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.MatchCollection matches = regex.Matches(Gossiptext);
            if (matches.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match match in matches)
                    Logging.Write(match.Value);
            }
            else
            {
                Logging.Write("Совпадений не найдено");
            }*/
            //match = matches.fir
            string chislotext = match.ToString();
            /*if(GossipText.Contains("решением"))
                chislotext = GossipText.Remove(0, GossipText.IndexOf("решением ") + 9);*/
            /*            if (GossipText.Contains("solution"))
                            chislotext = GossipText.Remove(0, GossipText.IndexOf("solution ") + 9);*/
            //System.Text.RegularExpressions.Regex.Replace()
            //chislotext = chislotext.Replace(" = ?", System.String.Empty);
            string[] words = chislotext.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int solution = 0;
            //var solution = System.Int32.Parse(words[0].ToString()) + words[1] + System.Int32.Parse(words[2].ToString());
            switch (words[1].ToString())
            {
                case "+":
                    solution = System.Int32.Parse(words[0].ToString()) + System.Int32.Parse(words[2].ToString());
                    break;
                case "-":
                    solution = System.Int32.Parse(words[0].ToString()) - System.Int32.Parse(words[2].ToString());
                    break;
                case "*":
                    solution = System.Int32.Parse(words[0].ToString()) * System.Int32.Parse(words[2].ToString());
                    break;
                case "/":
                    solution = System.Int32.Parse(words[0].ToString()) / System.Int32.Parse(words[2].ToString());
                    break;
            }
            string cifra = solution.ToString();
            //int cifraint = System.Int32.Parse(cifrachar.ToString());
            //var cifrakvp = DicNumbers.FirstOrDefault(v => v.Key == cifra);
            var DicNumber = number.DicNumbers.FirstOrDefault(v => v.digit == cifra);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i).ToLower() != "" && (GossipTitleButtonText(i).ToLower() == DicNumber.english || GossipTitleButtonText(i).ToLower() == DicNumber.russian || GossipTitleButtonText(i).ToLower() == DicNumber.digit))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }

    void Capcha3()
    {
        if (GossipText.Contains("выберите строку с цифрой ") || GossipText.Contains("select the row with the number"))
        {
            LOG("выберите строку с цифрой");
            print("выберите строку с цифрой");
            if (number.DicNumbers.Count(n => GossipText.Contains(n.digit) || GossipText.Contains(n.russian) || GossipText.Contains(n.english)) > 0)
            {
                LOG("number.DicNumbers.Count > 0");
                int digit;
                var DicNumber = number.DicNumbers.FirstOrDefault(n => GossipText.Contains(n.digit) || GossipText.Contains(n.russian) || GossipText.Contains(n.english));
                
                if (DicNumber != null)
                {
                    LOG("Ответ: " + DicNumber.digit);
                    for (int i = 1; i <= ButtonCount; i++)
                    {
                        LOG("проверяем кнопку " + i + " в ней текст " + GossipTitleButtonText(i).ToLower());
                        print("проверяем кнопку " + i + " в ней текст " + GossipTitleButtonText(i).ToLower());
                        if (GossipTitleButtonText(i).ToLower() == (DicNumber.digit) || GossipTitleButtonText(i).ToLower() == (DicNumber.russian) || GossipTitleButtonText(i).ToLower() == (DicNumber.english))
                        {
                            Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                            LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                            print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                            CloseGossip();
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }

    }
    /*void Capcha4()
    {    //Select the largest number
        //var GossipText = "Выберите самое большое число";
        if (GossipText.Contains("самое большое") || GossipText.Contains("largest"))
        {
            LOG("Выберите самое большое число | Select the largest number");
            print("Выберите самое большое число | Select the largest number");
            var ButtonList = new List<int>();
            *//*for (int i = 1; i <= ButtonCount; i++)
            {
                LOG("GossipTitleButtonText(" + i + ") = " + GossipTitleButtonText(i));
                print("GossipTitleButtonText(" + i + ") = " + GossipTitleButtonText(i));
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i) != "nil")
                {
                    var kvpnomer = number.DicNumbers.FirstOrDefault(v => GossipTitleButtonText(i).ToLower().Contains(v.digit) || GossipTitleButtonText(i).ToLower().Contains(v.english) || GossipTitleButtonText(i).ToLower().Contains(v.russian));
                    int nomer = 0;
                    bool checkint = System.Int32.TryParse(kvpnomer.digit,out nomer);
                    if (checkint)
                    {
                        ButtonList.Add(nomer);
                        LOG("Добавили в ButtonList цифру " + nomer);
                        print("Добавили в ButtonList цифру " + nomer);
                    }
                    else
                    {
                        LOG("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                        print("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                    }
                }
                else
                {
                    LOG("GossipTitleButtonText("+i+") пустой");
                    print("GossipTitleButtonText(" + i + ") пустой");
                }
            }*//*
            for(int i = 0; i <= Vseknopki.Count; i++)
            {
                LOG("GossipTitleButtonText(" + (i + 1) + ") = " + GossipTitleButtonText(i));
                print("GossipTitleButtonText(" + (i + 1) + ") = " + GossipTitleButtonText(i));
                var kvpnomer = number.DicNumbers.FirstOrDefault(v => Vseknopki[i].ToLower().Contains(v.digit) || Vseknopki[i].ToLower().Contains(v.english) || Vseknopki[i].ToLower().Contains(v.russian));
                Logging.Write((kvpnomer != null).ToString());
                int nomer = 0;
                bool checkint = System.Int32.TryParse(kvpnomer.digit, out nomer);

                *//*string sto = "100";
                bool checkint = System.Int32.TryParse(sto, out nomer);
                robotManager.Helpful.Var.SetVar("dbgOutput", checkint);
                Logging.Write(nomer);*//*
                if (checkint)
                {
                    ButtonList.Add(nomer);
                    LOG("Добавили в ButtonList цифру " + nomer);
                    print("Добавили в ButtonList цифру " + nomer);
                }
                else
                {
                    LOG("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                    print("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                }
            }
            int samoebolshoe = ButtonList.Max();
            var kvpsamoebolshoe = number.DicNumbers.FirstOrDefault(v => samoebolshoe.ToString() == v.digit);
            //Logging.Write("ButtonCount " + ButtonCount);
            LOG("Otvet v 4isle " + kvpsamoebolshoe.digit);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if ((GossipTitleButtonText(i).ToLower() == kvpsamoebolshoe.digit || GossipTitleButtonText(i).ToLower() == kvpsamoebolshoe.english || GossipTitleButtonText(i).ToLower() == kvpsamoebolshoe.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }

        }

    }*/

    public List<string> Vseknopki2 = new List<string>
    {
        "1"
        ,"Hundred"
        ,"4"
        ,"Zero"
    };

    void Capcha4test()
    {    //Select the largest number
        var GossipText = "Select the largest number";
        if (GossipText.Contains("самое большое") || GossipText.Contains("largest"))
        {
            LOG("Выберите самое большое число | Select the largest number");
            print("Выберите самое большое число | Select the largest number");
            /* var ButtonList = new List<int>();
             for (int i = 0; i <= Vseknopki2.Count - 1; i++)
             {
                 //LOG("GossipTitleButtonText(" + (i + 1) + ") = " + GossipTitleButtonText(i));
                 //print("GossipTitleButtonText(" + (i + 1) + ") = " + GossipTitleButtonText(i));
                 var kvpnomer = number.DicNumbers.FirstOrDefault(v => Vseknopki2[i].ToLower().Contains(v.digit) || Vseknopki2[i].ToLower().Contains(v.english) || Vseknopki2[i].ToLower().Contains(v.russian));
                 Logging.Write((kvpnomer != null).ToString());
                 int nomer = 0;
                 bool checkint = System.Int32.TryParse(kvpnomer.digit, out nomer);

                 *//*string sto = "100";
                 bool checkint = System.Int32.TryParse(sto, out nomer);
                 robotManager.Helpful.Var.SetVar("dbgOutput", checkint);
                 Logging.Write(nomer);*//*
                 if (checkint)
                 {
                     ButtonList.Add(nomer);
                     LOG("Добавили в ButtonList цифру " + nomer);
                     print("Добавили в ButtonList цифру " + nomer);
                 }
                 else
                 {
                     LOG("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                     print("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                 }
             }*/
            LOG("number.DicNumbers.Count = " + number.DicNumbers.Count);
            //number.DicNumbers.Count
            var ButtonList = new List<int>();
            for (int i = 0; i <= Vseknopki2.Count - 1; i++)
            {
                number kvpnomer = null;
                foreach (number nomer in number.DicNumbers)
                {
                    if (Vseknopki2[i].ToLower() == nomer.digit || Vseknopki2[i].ToLower() == nomer.english || Vseknopki2[i].ToLower() == nomer.russian)
                    {
                        LOG("Кнопка " + i + " есть в number.DicNumbers");
                        print("Кнопка " + i + " есть в number.DicNumbers");
                        //sendDiscordMessageCaptcha(FormatLua);
                        kvpnomer = nomer;
                        break;
                    }
                }
                Logging.Write("kvpnomer != null " + (kvpnomer != null).ToString());
                if (kvpnomer != null)
                {
                    int nomer = 0;
                    bool checkint = System.Int32.TryParse(kvpnomer.digit, out nomer);

                    /*string sto = "100";
                    bool checkint = System.Int32.TryParse(sto, out nomer);
                    robotManager.Helpful.Var.SetVar("dbgOutput", checkint);
                    Logging.Write(nomer);*/
                    if (checkint)
                    {
                        ButtonList.Add(nomer);
                        LOG("Добавили в ButtonList цифру " + nomer);
                        print("Добавили в ButtonList цифру " + nomer);
                    }
                    else
                    {
                        LOG("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                        print("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                    }
                }
            }
            int samoebolshoe = ButtonList.Max();
            Logging.Write("int samoebolshoe = ButtonList.Max()");
            var kvpsamoebolshoe = number.DicNumbers.FirstOrDefault(v => samoebolshoe.ToString() == v.digit);
            Logging.Write("int samoebolshoe = ButtonList.Max()");
            //Logging.Write("ButtonCount " + ButtonCount);
            LOG("Otvet v 4isle " + kvpsamoebolshoe.digit);
            for (int i = 0; i <= Vseknopki2.Count - 1; i++)
            {
                Logging.Write("Проверяем кнопку " + i);
                if ((Vseknopki2[i].ToLower() == kvpsamoebolshoe.digit || Vseknopki2[i].ToLower() == kvpsamoebolshoe.english || Vseknopki2[i].ToLower() == kvpsamoebolshoe.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + Vseknopki2[i], i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + Vseknopki2[i], i));
                    CloseGossip();
                    break;
                }
            }

        }

    }
    
    void Capcha4()
    {    //Select the largest number
        //var GossipText = "Select the largest number";
        
        if (GossipText.Contains("самое большое") || GossipText.Contains("largest"))
        {
            LOG("Выберите самое большое число | Select the largest number");
            print("Выберите самое большое число | Select the largest number");
            var ButtonList = new List<int>();
            for  (int i = 1; i<= ButtonCount; i++)
            {
                if(GossipTitleButtonText(i) != "")
                {
                    Vseknopki.Add(GossipTitleButtonText(i));
                    LOG("Vseknopki.Add " + GossipTitleButtonText(i));
                    print("Vseknopki.Add " + GossipTitleButtonText(i));
                }
            }
            for (int i = 0; i <= Vseknopki.Count - 1; i++)
            {
                //LOG("GossipTitleButtonText(" + (i + 1) + ") = " + GossipTitleButtonText(i));
                //print("GossipTitleButtonText(" + (i + 1) + ") = " + GossipTitleButtonText(i));
                //var kvpnomer = number.DicNumbers.FirstOrDefault(v => Vseknopki[i].ToLower() == v.digit || Vseknopki[i].ToLower() == v.english || Vseknopki[i].ToLower() == v.russian);
                number kvpnomer = null;
                LOG("Проверяем кнопку " + Vseknopki[i].ToLower());
                foreach (number nomer in number.DicNumbers)
                {
                    //LOG("Проверяем number " + nomer.digit);
                    if (Vseknopki[i].ToLower() == nomer.digit || Vseknopki[i].ToLower() == nomer.english || Vseknopki[i].ToLower() == nomer.russian)
                    {
                        LOG("Кнопка " + i + " есть в number.DicNumbers");
                        print("Кнопка " + i + " есть в number.DicNumbers");
                        //sendDiscordMessageCaptcha(FormatLua);
                        kvpnomer = nomer;
                        break;
                    }
                }
                Logging.Write("kvpnomer != null " + (kvpnomer != null).ToString());
                if (kvpnomer != null)
                {
                    int nomer = 0;
                    bool checkint = System.Int32.TryParse(kvpnomer.digit, out nomer);

                    /*string sto = "100";
                    bool checkint = System.Int32.TryParse(sto, out nomer);
                    robotManager.Helpful.Var.SetVar("dbgOutput", checkint);
                    Logging.Write(nomer);*/
                    if (checkint)
                    {
                        ButtonList.Add(nomer);
                        LOG("Добавили в ButtonList цифру " + nomer);
                        print("Добавили в ButtonList цифру " + nomer);
                    }
                    else
                    {
                        LOG("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                        print("System.Int32.TryParse(kvpnomer.digit,out nomer) не удался");
                    }
                }
            }
            int samoebolshoe = ButtonList.Max();
            //Logging.Write("int samoebolshoe = ButtonList.Max()");
            var kvpsamoebolshoe = number.DicNumbers.FirstOrDefault(v => samoebolshoe.ToString() == v.digit);
            //Logging.Write("int samoebolshoe = ButtonList.Max()");
            //Logging.Write("ButtonCount " + ButtonCount);
            LOG("Otvet v 4isle " + kvpsamoebolshoe.digit);
            for (int i = 1; i <= ButtonCount; i++)
            {
                Logging.Write("Проверяем кнопку " + (i));

                if ((GossipTitleButtonText(i).ToLower() == kvpsamoebolshoe.digit || GossipTitleButtonText(i).ToLower() == kvpsamoebolshoe.english || GossipTitleButtonText(i).ToLower() == kvpsamoebolshoe.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", (i + 1)));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                    break;
                }




/*                if ((Vseknopki[i].ToLower() == kvpsamoebolshoe.digit || Vseknopki[i].ToLower() == kvpsamoebolshoe.english || Vseknopki[i].ToLower() == kvpsamoebolshoe.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", (i + 1)));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + Vseknopki[i], i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + Vseknopki[i], i));
                    CloseGossip();
                    break;
                }*/
            }

        }

    }



    void Capcha5()
    {
        //Andrey lives on the 7th floor, and Alyona lives on the 3rd floor below. What floor does Alyona live on
        //Андрей живет на 7 этаже, а Алена на 3 этажа ниже. На каком этаже живет Алена ?
        //var GossipText = "Андрей живет на 7 этаже, а Алена на 3 этажа ниже. На каком этаже живет Алена ?";

        if (GossipText.Contains("этаж") || GossipText.Contains("floor"))
        {
            LOG("Andrey lives on the 7th floor, and Alyona lives on the 3rd floor below. What floor does Alyona live on");
            print("этажи");
            //var GossipText = "Andrey lives on the 7th floor, and Alyona lives on the 3rd floor below. What floor does Alyona live on";
            //string[] words = GossipText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] digits = System.Text.RegularExpressions.Regex.Split(GossipText, @"\D+");
            List<int> tmp = new List<int>();
            for (int i = 0; i <= digits.Length - 1; i++)
            {
                int outint = 0;
                bool haveint = System.Int32.TryParse(digits[i], out outint);
                if (haveint)
                    tmp.Add(outint);
                //test
            }
            /*            for (int i = 0; i <= words.Length - 1; i++)
                        {
                            int outint = 0;
                            bool haveint = System.Int32.TryParse(words[i], out outint);
                            if (haveint)
                                tmp.Add(outint);
                        }*/
            /*            foreach (var i in tmp)
                        {
                            Logging.Write(i.ToString());
                        }*/
            int solution = 0;
            if (GossipText.Contains("вместе") || GossipText.Contains("summ"))
            {
                solution = tmp.Sum();
            }
            if (GossipText.Contains("ниже") || GossipText.Contains("below"))
            {
                solution = tmp[0] - tmp[1];
            }
            if (GossipText.Contains("выше") || GossipText.Contains("above"))
            {
                solution = tmp[0] + tmp[1];
            }
            string cifra = solution.ToString();
            //int cifraint = System.Int32.Parse(cifrachar.ToString());
            var cifrakvp = number.DicNumbers.FirstOrDefault(v => v.digit == cifra);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i).ToLower() != "" && (GossipTitleButtonText(i).ToLower() == cifrakvp.digit || GossipTitleButtonText(i).ToLower() == cifrakvp.english || GossipTitleButtonText(i).ToLower() == cifrakvp.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }

    }
    void Capcha6()
    {
        //There were 10 plates on the table and 6 fewer in the sink. How many plates were there in the sink

        //var GossipText = "There were 10 plates on the table and 6 fewer in the sink. How many plates were there in the sink";
        if (GossipText.Contains("тарел") || GossipText.Contains("яблон") || GossipText.Contains("plate") || GossipText.Contains("appl"))
        {
            if (GossipText.Contains("plate"))
            {
                print("тарелки");
                LOG("Tарелки");
            }    
                
            if (GossipText.Contains("appl"))
            {
                print("яблоки");
                LOG("On one branch of the apple tree hung 6 apples, and on the other - 7. How many apples were on both branches of the apple tree");
            }
                
            //var GossipText = "There were 10 plates on the table and 6 fewer in the sink. How many plates were there in the sink";
            //var GossipText = "On one branch of the apple tree hung 6 apples, and on the other - 7. How many apples were on both branches of the apple tree";
            string[] words = GossipText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> tmp = new List<int>();
            /*for (int i = 0; i <= words.Length - 1; i++)
            {
                int outint = 0;
                bool haveint = System.Int32.TryParse(words[i], out outint);

                if (haveint)
                    tmp.Add(outint);
            }*/
            for (int i = 0; i <= words.Length - 1; i++)
            {
                int outint = 0;
                // bool haveint = System.Char.IsDigit(GossipText[i]);
                string wordwhithoutsymbols = System.Text.RegularExpressions.Regex.Replace(words[i], @"\D", "");
                bool haveint = System.Int32.TryParse(wordwhithoutsymbols, out outint);

                if (haveint)
                {
                    tmp.Add(outint);
                    //tmp.Add(System.Int32.Parse(GossipText[i].ToString()));
                }
                /*            else
                            {
                                if (words[i].Length > 1)
                                {
                                    string slovo = words[i];
                                    for (int b = 0; b <= words[i].Length; b++)
                                    {
                                        bool isdigit = System.Char.IsDigit(slovo[b]);
                                        if (isdigit)
                                            tmp.Add(System.Int32.Parse(slovo[b].ToString()));
                                    }
                                }
                            }*/

            }
            int solution = 0;
            if (GossipText.Contains("вместе") || GossipText.Contains("на обеих") || GossipText.Contains("how many"))
            {
                solution = tmp.Sum();
            }
            if (GossipText.Contains("меньше") || GossipText.Contains("fewer"))
            {
                solution = tmp[0] - tmp[1];
            }
            if (GossipText.Contains("больше") || GossipText.Contains("more"))
            {
                solution = tmp[0] + tmp[1];
            }
            string cifra = solution.ToString();
            LOG("ответ - " + cifra);
            //int cifraint = System.Int32.Parse(cifrachar.ToString());
            var cifrakvp = number.DicNumbers.FirstOrDefault(v => v.digit == cifra);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i).ToLower() != "" && (GossipTitleButtonText(i).ToLower() == cifrakvp.digit || GossipTitleButtonText(i).ToLower() == cifrakvp.english || GossipTitleButtonText(i).ToLower() == cifrakvp.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }

    /*public static List<string> TopPutterns = new List<string>()
    {
        "_88888_",//верхушка нуля = верхушка восьмерки
        "_8_",//верхушка единицы
        "_8888___",//верхушка двойки
        "888888_",//верхушка тройки = верхушка пятерки
        "____88__",//верхушка четверки
        "888888_",//верхушка пятерки = верхушка тройки
        "___888_",//верхушка шестерки
        "8888888",//верхушка семерки
        "_88888_",//верхушка восьмерки = верхушка нуля
        "_8888__",//верхушка девятки
    };*/

    /*  public static List<string> MidPutterns = new List<string>()
      {
          "88___88",//середина нуля 
          "_88",//середина единицы
          "____888",//середина двойки
          "__8888_",//середина тройки 
          "_88__8__",//середина четверки
          "888888_",//середина пятерки 
          "8888888",//середина шестерки
          "___888__",//середина семерки
          "_88888_",//середина восьмерки 
          "_888888",//середина девятки
      };*/



    /*
        public static Dictionary<string,string> FirstLinePutterns = new Dictionary<string, string>()
        {
            {"0","_88888_"    },//верхушка нуля = верхушка восьмерки
            {"1","_8_"        },//верхушка единицы
            {"2","_8888__"   },//верхушка двойки
            {"3",@"\W{0,}888888_"    },//верхушка тройки = верхушка пятерки
            {"4","____88__"   },//верхушка четверки
            {"5",@"\W{0,}888888_"    },//верхушка пятерки = верхушка тройки
            {"6","__888__"    },//верхушка шестерки
            {"7",@"\W{0,}8888888\W{0,}"    },//верхушка семерки
            {"8","_88888_"    },//верхушка восьмерки = верхушка нуля
            {"9","_______"    },//верхушка девятки
        };
        public static Dictionary<string, string> SecondLinePutterns = new Dictionary<string, string>()
        {
            {"0",@"\W{0,}88___88\W{0,}"    },//2-я строчка нуля
            {"1",@"\W{0,}888\W{0,}"        },//2-я строчка единицы
            {"2",@"\W{0,}888888_"   },//2-я строчка двойки
            {"3",@"___8888\W{0,}"    },//2-я строчка тройки 
            {"4","___888__"   },//2-я строчка четверки
            {"5",@"\W{0,}88____"    },//2-я строчка пятерки 
            {"6","_88____"    },//2-я строчка шестерки
            {"7",@"____888\W{0,}"    },//2-я строчка семерки
            {"8",@"\W{0,}88___88\W{0,}"    },//2-я строчка восьмерки 
            {"9",@"_8888__"    },//2-я строчка девятки
        };
        public static Dictionary<string, string> MidPutterns = new Dictionary<string, string>()
        {
            {"0",@"\W{0,}88___88\W{0,}"}, //середина нуля 
            {"1",@"_88\W{0,}"},     //середина единицы
            {"2",@"____888\W{0,}"}, //середина двойки
            {"3","__8888_"}, //середина тройки 
            {"4","_88__8__"}, //середина четверки
            {"5",@"\W{0,}888888_"}, //середина пятерки 
            {"6",@"\W{0,}8888888\W{0,}"}, //середина шестерки
            {"7","___888_"}, //середина семерки
            {"8","_88888_"}, //середина восьмерки 
            {"9",@"\W{0,}88___88\W{0,}"}, //середина девятки
        };
        public static Dictionary<string, string> fourthlinePutterns = new Dictionary<string, string>()
        {
            {"0",@"\W{0,}88___88\W{0,}"}, //4-я строчка нуля 
            {"1",@"_88\W{0,}"},     //4-я строчка единицы
            {"2","_8888__"}, //4-я строчка двойки
            {"3",@"____888\W{0,}"}, //4-я строчка тройки 
            {"4",@"\W{0,}88888888\W{0,}"}, //4-я строчка  четверки
            {"5",@"___8888\W{0,}"}, //4-я строчка  пятерки 
            {"6",@"\W{0,}88___88\W{0,}"}, //4-я строчка  шестерки
            {"7","__888__"}, //4-я строчка  семерки
            {"8",@"\W{0,}88___88\W{0,}"}, //4-я строчка  восьмерки 
            {"9",@"_888888"}, //4-я строчка  девятки
        };


        public static Dictionary<string, string> BottomPutterns = new Dictionary<string, string>()
        {
           {"0", "_88888_"},//низ нуля  = низ шестерки = низ восьмерки
           {"1", @"\W{0,}888\W{0,}"},//низ единицы
           {"2", @"\W{0,}8888888\W{0,}"},//низ двойки
           {"3", @"\W{0,}888888_"},//низ тройки 
           {"4", "___888__"},//низ четверки
           {"5", @"\W{0,}888888_"},//низ пятерки 
           {"6", "_88888_"},//низ шестерки = низ нуля = низ восьмерки
           {"7", "_888___"},//низ семерки
           {"8", "_88888_"},//низ восьмерки = низ шестерки = низ нуля
           {"9", "____88_"},//низ девятки
        };
    */


    /*   public static Dictionary<string, string> FirstLinePutterns = new Dictionary<string, string>()
       {
           {"0",@"_\d{5}_"    },//верхушка нуля = верхушка восьмерки
           {"1",@"_\d{1}_"        },//верхушка единицы
           {"2",@"_\d{4}__"   },//верхушка двойки
           {"3",@"\W{0,}\d{6}_"    },//верхушка тройки = верхушка пятерки
           {"4",@"____\d{2}__"   },//верхушка четверки
           {"5",@"\W{0,}\\d{6}_"    },//верхушка пятерки = верхушка тройки
           {"6",@"__\d{3}__"    },//верхушка шестерки
           {"7",@"\W{0,}\\d{7}\W{0,}"    },//верхушка семерки
           {"8",@"_\d{5}_"    },//верхушка восьмерки = верхушка нуля
           {"9","_______"    },//верхушка девятки
       };
       public static Dictionary<string, string> SecondLinePutterns = new Dictionary<string, string>()
       {
           {"0",@"\W{0,}\d{2}___\d{2}\W{0,}"    },//2-я строчка нуля
           {"1",@"\W{0,}\d{3}\W{0,}"        },//2-я строчка единицы
           {"2",@"\W{0,}\d{6}_"   },//2-я строчка двойки
           {"3",@"___\d{4}\W{0,}"    },//2-я строчка тройки 
           {"4",@"___\d{3}__"   },//2-я строчка четверки
           {"5",@"\W{0,}\d{2}____"    },//2-я строчка пятерки 
           {"6",@"_\d{2}____"    },//2-я строчка шестерки
           {"7",@"____\d{3}\W{0,}"    },//2-я строчка семерки
           {"8",@"\W{0,}\d{2}___\d{2}\W{0,}"    },//2-я строчка восьмерки 
           {"9",@"_\d{4}__"    },//2-я строчка девятки
       };
       public static Dictionary<string, string> MidPutterns = new Dictionary<string, string>()
       {
           {"0",@"\W{0,}\d{2}___\d{2}\W{0,}"}, //середина нуля 
           {"1",@"_\d{2}\W{0,}"},     //середина единицы
           {"2",@"____\d{3}\W{0,}"}, //середина двойки
           {"3",@"__\d{4}_"}, //середина тройки 
           {"4",@"_\d{2}__\d{1}__"}, //середина четверки
           {"5",@"\W{0,}\d{6}_"}, //середина пятерки 
           {"6",@"\W{0,}\d{7}\W{0,}"}, //середина шестерки
           {"7",@"___\d{3}_"}, //середина семерки
           {"8",@"_\d{5}_"}, //середина восьмерки 
           {"9",@"\W{0,}\d{2}___\d{2}\W{0,}"}, //середина девятки
       };
       public static Dictionary<string, string> fourthlinePutterns = new Dictionary<string, string>()
       {
           {"0",@"\W{0,}\d{2}___\d{2}\W{0,}"}, //4-я строчка нуля 
           {"1",@"_\d{2}\W{0,}"},     //4-я строчка единицы
           {"2",@"_\d{4}__"}, //4-я строчка двойки
           {"3",@"____\d{3}\W{0,}"}, //4-я строчка тройки 
           {"4",@"\W{0,}\d{8}\W{0,}"}, //4-я строчка  четверки
           {"5",@"___\d{4}\W{0,}"}, //4-я строчка  пятерки 
           {"6",@"\W{0,}\d{2}___\d{2}\W{0,}"}, //4-я строчка  шестерки
           {"7",@"__\d{3}__"}, //4-я строчка  семерки
           {"8",@"\W{0,}\d{2}___\d{2}\W{0,}"}, //4-я строчка  восьмерки 
           {"9",@"_\d{6}"}, //4-я строчка  девятки
       };


       public static Dictionary<string, string> BottomPutterns = new Dictionary<string, string>()
       {
          {"0", @"_\d{5}_"},//низ нуля  = низ шестерки = низ восьмерки
          {"1", @"\W{0,}\d{3}\W{0,}"},//низ единицы
          {"2", @"\W{0,}\d{7}\W{0,}"},//низ двойки
          {"3", @"\W{0,}\d{6}_"},//низ тройки 
          {"4", @"___\d{3}__"},//низ четверки
          {"5", @"\W{0,}\d{6}_"},//низ пятерки 
          {"6", @"_\d{5}_"},//низ шестерки = низ нуля = низ восьмерки
          {"7", @"_\d{3}___"},//низ семерки
          {"8", @"_\d{5}_"},//низ восьмерки = низ шестерки = низ нуля
          {"9", @"____\d{2}_"},//низ девятки
       };

   */


    /*public static Dictionary<string, string> FirstLinePutterns = new Dictionary<string, string>()
    {
        {"0",@"_[^_]{5}_"    },//верхушка нуля = верхушка восьмерки
        {"1",@"_[^_]{1}_"        },//верхушка единицы
        {"2",@"_[^_]{4}__"   },//верхушка двойки
        {"3",@"\W{0,}[^_]{6}_"    },//верхушка тройки = верхушка пятерки
        {"4",@"____[^_]{2}__"   },//верхушка четверки
        {"5",@"\W{0,}\[^_]{6}_"    },//верхушка пятерки = верхушка тройки
        {"6",@"__[^_]{3}__"    },//верхушка шестерки
        {"7",@"\W{0,}\[^_]{7}\W{0,}"    },//верхушка семерки
        {"8",@"_[^_]{5}_"    },//верхушка восьмерки = верхушка нуля
        {"9","_______"    },//верхушка девятки
    };
    public static Dictionary<string, string> SecondLinePutterns = new Dictionary<string, string>()
    {
        {"0",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"    },//2-я строчка нуля
        {"1",@"\W{0,}[^_]{3}\W{0,}"        },//2-я строчка единицы
        {"2",@"\W{0,}[^_]{6}_"   },//2-я строчка двойки
        {"3",@"___[^_]{4}\W{0,}"    },//2-я строчка тройки 
        {"4",@"___[^_]{3}__"   },//2-я строчка четверки
        {"5",@"\W{0,}[^_]{2}____"    },//2-я строчка пятерки 
        {"6",@"_[^_]{2}____"    },//2-я строчка шестерки
        {"7",@"____[^_]{3}\W{0,}"    },//2-я строчка семерки
        {"8",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"    },//2-я строчка восьмерки 
        {"9",@"_[^_]{4}__"    },//2-я строчка девятки
    };
    public static Dictionary<string, string> MidPutterns = new Dictionary<string, string>()
    {
        {"0",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"}, //середина нуля 
        {"1",@"_[^_]{2}\W{0,}"},     //середина единицы
        {"2",@"____[^_]{3}\W{0,}"}, //середина двойки
        {"3",@"__[^_]{4}_"}, //середина тройки 
        {"4",@"_[^_]{2}__[^_]{1}__"}, //середина четверки
        {"5",@"\W{0,}[^_]{6}_"}, //середина пятерки 
        {"6",@"\W{0,}[^_]{7}\W{0,}"}, //середина шестерки
        {"7",@"___[^_]{3}_"}, //середина семерки
        {"8",@"_[^_]{5}_"}, //середина восьмерки 
        {"9",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"}, //середина девятки
    };
    public static Dictionary<string, string> fourthlinePutterns = new Dictionary<string, string>()
    {
        {"0",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"}, //4-я строчка нуля 
        {"1",@"_[^_]{2}\W{0,}"},     //4-я строчка единицы
        {"2",@"_[^_]{4}__"}, //4-я строчка двойки
        {"3",@"____[^_]{3}\W{0,}"}, //4-я строчка тройки 
        {"4",@"\W{0,}[^_]{8}\W{0,}"}, //4-я строчка  четверки
        {"5",@"___[^_]{4}\W{0,}"}, //4-я строчка  пятерки 
        {"6",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"}, //4-я строчка  шестерки
        {"7",@"__[^_]{3}__"}, //4-я строчка  семерки
        {"8",@"\W{0,}[^_]{2}___[^_]{2}\W{0,}"}, //4-я строчка  восьмерки 
        {"9",@"_[^_]{6}"}, //4-я строчка  девятки
    };


    public static Dictionary<string, string> BottomPutterns = new Dictionary<string, string>()
    {
       {"0", @"_[^_]{5}_"},//низ нуля  = низ шестерки = низ восьмерки _88888_ _$$$$$_
       {"1", @"\W{0,}[^_]{3}\W{0,}"},//низ единицы
       {"2", @"\W{0,}[^_]{7}\W{0,}"},//низ двойки
       {"3", @"\W{0,}[^_]{6}_"},//низ тройки 
       {"4", @"___[^_]{3}__"},//низ четверки
       {"5", @"\W{0,}[^_]{6}_"},//низ пятерки 
       {"6", @"_[^_]{5}_"},//низ шестерки = низ нуля = низ восьмерки
       {"7", @"_[^_]{3}___"},//низ семерки
       {"8", @"_[^_]{5}_"},//низ восьмерки = низ шестерки = низ нуля
       {"9", @"____[^_]{2}_"},//низ девятки
    };
*/
    //([_=~]|:{2}|;{2}) - разделитель
    //[^_=;:~]
    //public string 


    public Dictionary<int, int> DigitMaxLengthWithSpaces = new Dictionary<int, int>
    {
        {0 ,8 },
        {1 ,4 },
        {2 ,8 },
        {3 ,8 },
        {4 ,9 },
        {5 ,9 },
        {6 ,9 },
        {7 ,8 },
        {8 ,8 },
        {9 ,8 },
    };
    public static Dictionary<string, string> FirstLinePutterns = new Dictionary<string, string>()
    
    {
        {"0",@"([_=~]|:{2}|;{2})[^_=;:~]{5}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//верхушка нуля = верхушка восьмерки
        {"1",@"([_=~]|:{2}|;{2})[^_=;:~]{1}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"        },//верхушка единицы
        {"2",@"([_=~]|:{2}|;{2})[^_=;:~]{4}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"   },//верхушка двойки
        {"3",@"[^_=;:~]{6}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//верхушка тройки
        {"4",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"   },//верхушка четверки
        {"5",@"[^_=;:~]{6}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//верхушка пятерки 
        {"6",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//верхушка шестерки
        {"7",@"[^_=;:~]{7}([_=~]|:{2}|;{2})"    },//верхушка семерки
        {"8",@"([_=~]|:{2}|;{2})[^_=;:~]{5}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//верхушка восьмерки = верхушка нуля
        {"9","([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//верхушка девятки
    };
    public static Dictionary<string, string> SecondLinePutterns = new Dictionary<string, string>()
    {
        {"0",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"    },//2-я строчка нуля
        {"1",@"[^_=;:~]{3}([_=~]|:{2}|;{2})"        },//2-я строчка единицы
        {"2",@"[^_=;:~]{6}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"   },//2-я строчка двойки
        {"3",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{4}([_=~]|:{2}|;{2})"    },//2-я строчка тройки 
        {"4",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"   },//2-я строчка четверки
        {"5",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//2-я строчка пятерки 
        {"6",@"([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//2-я строчка шестерки
        {"7",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})"    },//2-я строчка семерки
        {"8",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"    },//2-я строчка восьмерки 
        {"9",@"([_=~]|:{2}|;{2})[^_=;:~]{4}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"    },//2-я строчка девятки
    };
    public static Dictionary<string, string> ThirdLinePutterns = new Dictionary<string, string>()
    {
        {"0",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"}, //середина нуля 
        {"1",@"([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"},     //середина единицы
        {"2",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})"}, //середина двойки
        {"3",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{4}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //середина тройки 
        {"4",@"([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{1}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //середина четверки
        {"5",@"[^_=;:~]{6}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //середина пятерки 
        {"6",@"[^_=;:~]{7}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //середина шестерки
        {"7",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //середина семерки
        {"8",@"([_=~]|:{2}|;{2})[^_=;:~]{5}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //середина восьмерки 
        {"9",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"}, //середина девятки
    };
    public static Dictionary<string, string> fourthlinePutterns = new Dictionary<string, string>()
    {
        {"0",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"}, //4-я строчка нуля 
        {"1",@"([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"},     //4-я строчка единицы
        {"2",@"([_=~]|:{2}|;{2})[^_=;:~]{4}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //4-я строчка двойки
        {"3",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})"}, //4-я строчка тройки 
        {"4",@"[^_=;:~]{8}([_=~]|:{2}|;{2})"}, //4-я строчка  четверки
        {"5",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{4}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //4-я строчка  пятерки 
        {"6",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //4-я строчка  шестерки
        {"7",@"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"}, //4-я строчка  семерки
        {"8",@"[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})"}, //4-я строчка  восьмерки 
        {"9",@"([_=~]|:{2}|;{2})[^_=;:~]{6}([_=~]|:{2}|;{2})"}, //4-я строчка  девятки
    };


    public static Dictionary<string, string> FiveLinePutterns = new Dictionary<string, string>()
    {
       {"0", @"([_=~]|:{2}|;{2})[^_=;:~]{5}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ нуля  = низ шестерки = низ восьмерки ([_=~]|:{2}|;{2})88888([_=~]|:{2}|;{2}) ([_=~]|:{2}|;{2})$$$$$([_=~]|:{2}|;{2})
       {"1", @"[^_=;:~]{3}([_=~]|:{2}|;{2})"},//низ единицы
       {"2", @"[^_=;:~]{7}([_=~]|:{2}|;{2})"},//низ двойки
       {"3", @"[^_=;:~]{6}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ тройки 
       {"4", @"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ четверки
       {"5", @"[^_=;:~]{6}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ пятерки 
       {"6", @"([_=~]|:{2}|;{2})[^_=;:~]{5}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ шестерки = низ нуля = низ восьмерки
       {"7", @"([_=~]|:{2}|;{2})[^_=;:~]{3}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ семерки
       {"8", @"([_=~]|:{2}|;{2})[^_=;:~]{5}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ восьмерки = низ шестерки = низ нуля
       {"9", @"([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})[^_=;:~]{2}([_=~]|:{2}|;{2})([_=~]|:{2}|;{2})"},//низ девятки
    };

    public void testline()
    {
        //[^_=] - число
        //[_=] - между числами
        string line = "=%%%%%=";
        string pattern = @"[_=(::)][^_=:~]{5}[_=(::)]";
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Match match = regex.Match(line);
        robotManager.Helpful.Var.SetVar("dbgOutput", match.Success);
    }
    //максимальная длина первой цифры
    public Dictionary<int, int> DigitMaxLength = new Dictionary<int, int>
    {
        {0 ,7 },
        {1 ,3 },
        {2 ,7 },
        {3 ,7 },
        {4 ,8 },
        {5 ,7 },
        {6 ,7 },
        {7 ,7 },
        {8 ,7 },
        {9 ,7 },
    };



   


    public Dictionary<int, Dictionary<int, int>> DigitMaxLengthColon = new Dictionary<int, Dictionary<int, int>>//key - число, value - это Dictionary<номер сроки, макс дляна>
    {
        {0 ,new Dictionary<int,int>{  {1, 9}, { 2, 10 }, { 3, 10 }, { 4, 10 }, { 5, 9 }, } },
        {1 ,new Dictionary<int,int>{  {1, 5}, { 2, 3 }, { 3, 4 }, { 4, 4 }, { 5, 3 }, } }, // в словаре key - номер строки, value - макс длина числа для этой строки с учетом двоиточий
        {2 ,new Dictionary<int,int>{  {1, 10}, { 2, 8 }, { 3, 11 }, { 4, 10 }, { 5, 5 }, } },
        {3 ,new Dictionary<int,int>{  {1, 8}, { 2, 10 }, { 3, 10 }, { 4, 11 }, { 5, 8 }, } },
        {5 ,new Dictionary<int,int>{  {1, 8}, { 2, 12 }, { 3, 8 }, { 4, 10 }, { 5, 8 }, } },
        {6 ,new Dictionary<int,int>{  {1, 11}, { 2, 12 }, { 3, 7 }, { 4, 10 }, { 5, 9 }, } },
        {7 ,new Dictionary<int,int>{  {1, 7}, { 2, 11 }, { 3, 11 }, { 4, 11 }, { 5, 11 }, } },
        {8 ,new Dictionary<int,int>{  {1, 9}, { 2, 10 }, { 3, 9 }, { 4, 10 }, { 5, 9 }, } },
        {9 ,new Dictionary<int,int>{  {1, 14}, { 2, 10 }, { 3, 10 }, { 4, 8 }, { 5, 12 }, } },
    };
    public string CheckDigit(List<string> LinestList,bool first, bool last)
    {
        string digit = "";
        Sovpadeniya.Clear();
       /* string stroka1 = list[0];
        string stroka2 = list[1];
        string stroka3 = list[2];
        string stroka4 = list[3];
        string stroka5 = list[4];*/
        for (int b = 0; b <= 9; b++)
        {
            Logging.Write("Цифра " + b);
            for (int i = 0; i <= 4; i++) //i - номер строки начина с 0
            {
                Logging.Write("Проверяем строку №" + (i + 1) + " на признаки числа " + b);
                int maxlength = DigitMaxLength[b];
                Logging.Write("Максимальная длина числа " + b + " = " + maxlength);
                /*if (LinestList[i].Contains("::"))
                {
                    Logging.Write("1");
                    maxlength = DigitMaxLengthColon[b][i + 1];
                    Logging.Write("Максимальная длина числа с двоеточиями " + b + " в строке №" + (i + 1) + " = " + maxlength);
                }
                else
                {
                    Logging.Write("Максимальная длина числа " + b + " = " + maxlength);
                }
                */
                string stroka = "";
                if(first)
                {
                    Logging.WriteDebug("if(first) start");
                    stroka = LinestList[i].Substring(0, maxlength);
                    Logging.WriteDebug("if(first) end");
                }
                if(last)
                {
                    //Logging.Write("1");
                    Logging.WriteDebug("if(last) start");
                    stroka = LinestList[i].Substring(LinestList[i].Length - maxlength - RazdelitelCount(CapchaLines), maxlength);
                    Logging.WriteDebug("if(last) end");
                    //Logging.Write("2");
                }
                Logging.Write("Обрезанная под число " + b + " строка №" + (i + 1) + ": [" + stroka + "]");
                Dictionary<string, string> tempdict = new Dictionary<string, string>();
                if (i == 0)
                {
                    tempdict = FirstLinePutterns;
                }
                else if(i == 1)
                {
                    tempdict = SecondLinePutterns;
                }
                else if (i == 2)
                {
                    tempdict = ThirdLinePutterns;
                }
                else if (i == 3)
                {
                    tempdict = fourthlinePutterns;
                }
                else if (i == 4)
                {
                    tempdict = FiveLinePutterns;
                }
                //Regex regex = new Regex(FirstLinePutterns[b.ToString()]);
                string pattern = tempdict[b.ToString()];
                Logging.Write("Pattern для числа " + b + "  на линии №" + (i + 1) + " = " + pattern);
                var match = Regex.Match(stroka, pattern);
                    if(match.Success)
                    {

                        if (Sovpadeniya.ContainsKey(b.ToString()))
                        {
                            
                            Sovpadeniya[b.ToString()]++;                           
                        }
                        else
                        {
                            Sovpadeniya.Add(b.ToString(), 1);
                        }
                        LOG("Обнаружено совпадение для числа " + b + " в строке №" + (i + 1) + " Всего совпадений " + Sovpadeniya[b.ToString()]);
                }
                
            }
        }
        LOG("Sovpadeniya.Values.Count = " + Sovpadeniya.Values.Count);
        int MaxValue = Sovpadeniya.Values.Max();
        foreach(var kvp in Sovpadeniya)
        {
            if(kvp.Value == MaxValue)
            {
                LOG("Максимальное число совпадений у цифры [" + kvp.Key + "] это " + kvp.Value);
                digit = kvp.Key;
            }
        }
        return digit;
    }
    //план - проверяются все цифры по очереди от 0 до 9, в цикле фор 4 раза по одной итерации для определения каждой цифры, после каждой проверки определяется цифра с наибольшим совпадением и все строки в списке LinestList обрезаются с начала на длину выбранной цифры
    public string CheckDigit(List<string> LinestList)
    {
        string digit = "";
        Sovpadeniya.Clear();
        /* string stroka1 = list[0];
         string stroka2 = list[1];
         string stroka3 = list[2];
         string stroka4 = list[3];
         string stroka5 = list[4];*/
        for (int a = 1; a <= 4; a++)
        {
            Logging.Write("Проверка цифры № " + a);
            for (int b = 0; b <= 9; b++)
            {
                Logging.Write("Начало цикла проверки цифры №" + a + " на совпадение с цифрой " + b);
                int digitlength = DigitMaxLengthWithSpaces[b]; //длина числа
                Logging.Write("Максимальная длина числа " + b + " = " + digitlength);
                for (int i = 0; i <= 4; i++) //i - номер строки начина с 0
                {
                    Logging.Write("Проверяем строку №" + (i + 1) + " на признаки числа " + b);
                    string stroka = "";
                    //Logging.WriteDebug("if(first) start");
                    try
                    {
                        stroka = LinestList[i].Substring(0, digitlength);
                    }
                    catch(Exception e)
                    {
                        Logging.WriteError("Исключение типа [" + e.Message + "] при попытке обрезать строку для числа " + b);
                        break;
                    }
                    //Logging.WriteDebug("if(first) end");
                    /*if (last)
                    {
                        //Logging.Write("1");
                        Logging.WriteDebug("if(last) start");
                        stroka = LinestList[i].Substring(LinestList[i].Length - digitlength - RazdelitelCount(CapchaLines), digitlength);
                        Logging.WriteDebug("if(last) end");
                        //Logging.Write("2");
                    }*/
                    Logging.Write("Обрезанная под число " + b + " строка №" + (i + 1) + ": [" + stroka + "]");
                    Dictionary<string, string> tempdict = new Dictionary<string, string>();
                    if (i == 0)
                    {
                        tempdict = FirstLinePutterns;
                    }
                    else if (i == 1)
                    {
                        tempdict = SecondLinePutterns;
                    }
                    else if (i == 2)
                    {
                        tempdict = ThirdLinePutterns;
                    }
                    else if (i == 3)
                    {
                        tempdict = fourthlinePutterns;
                    }
                    else if (i == 4)
                    {
                        tempdict = FiveLinePutterns;
                    }
                    //Regex regex = new Regex(FirstLinePutterns[b.ToString()]);
                    string pattern = tempdict[b.ToString()];
                    Logging.Write("Pattern для числа " + b + "  на линии №" + (i + 1) + " = " + pattern);
                    var match = Regex.Match(stroka, pattern);
                    if (match.Success)
                    {

                        if (Sovpadeniya.ContainsKey(b.ToString()))
                        {
                            Sovpadeniya[b.ToString()]++;
                        }
                        else
                        {
                            Sovpadeniya.Add(b.ToString(), 1);
                        }
                        LOG("Обнаружено совпадение для числа " + b + " в строке №" + (i + 1) + " Всего совпадений " + Sovpadeniya[b.ToString()]);
                    }

                }
            }
            LOG("Sovpadeniya.Values.Count = " + Sovpadeniya.Values.Count);
            int MaxValue = Sovpadeniya.Values.Max();
            string currentdigit = "0";
            foreach (var kvp in Sovpadeniya)
            {
                if (kvp.Value == MaxValue)
                {
                    LOG("Максимальное число совпадений у цифры [" + kvp.Key + "] это " + kvp.Value);
                    currentdigit = kvp.Key;
                    digit += kvp.Key;
                    break;
                }
            }
            //теперь обрезаем все строки в ListLines на значение MaxValue
            int SovpDigitLength = DigitMaxLengthWithSpaces[Convert.ToInt32(currentdigit)];
            for (int c = 0; c <= 4 && a < 4; c++)
            {
                LinestList[c] = LinestList[c].Remove(0, SovpDigitLength);
                Logging.Write("обрезали начало строки №" + (c + 1) + " на " + SovpDigitLength + " символов, теперь выглядит так : "+ LinestList[c]);
            }
            Sovpadeniya.Clear();
        }
        Logging.Write("Число = " + digit);
        return digit;
    }



    public string CheckAllDigits(List<string> list)
    {
        string digit = "";
        /* string stroka1 = list[0];
         string stroka2 = list[1];
         string stroka3 = list[2];
         string stroka4 = list[3];
         string stroka5 = list[4];*/
        for (int b = 0; b <= 9; b++)
        {
            Logging.Write("Цифра " + b);
            for (int i = 0; i <= 4; i++)
            {
                Logging.Write("Проверяем строку " + (i + 1) + " на признаки числа " + b);
                int maxlength = DigitMaxLength[b];
                Logging.Write("Максимальная длина числа " + b + " = " + maxlength);
                /*if (CapchaLines[i].Contains("::"))
                {
                    maxlength = DigitMaxLengthColon[b][i + 1];
                    Logging.Write("Максимальная длина числа с двоеточиями " + b + " в строке №" + (i + 1) + " = " + maxlength);
                }
                else
                {
                    Logging.Write("Максимальная длина числа " + b + " = " + maxlength);
                }*/
                string stroka = "";
                stroka = list[i].Substring(0, maxlength);
/*                if (last)
                {
                    //Logging.Write("1");
                    stroka = list[i].Substring((list[i].Length - maxlength - ChisloUnderlines(CapchaLines)), maxlength);
                    //Logging.Write("2");
                }*/
                Logging.Write("Обрезанная под число " + b + " строка №" + (i + 1) + " = " + stroka);
                Dictionary<string, string> tempdict = new Dictionary<string, string>();
                if (i == 0)
                {
                    tempdict = FirstLinePutterns;
                }
                else if (i == 1)
                {
                    tempdict = SecondLinePutterns;
                }
                else if (i == 2)
                {
                    tempdict = ThirdLinePutterns;
                }
                else if (i == 3)
                {
                    tempdict = fourthlinePutterns;
                }
                else if (i == 4)
                {
                    tempdict = FiveLinePutterns;
                }
                //Regex regex = new Regex(FirstLinePutterns[b.ToString()]);
                string pattern = tempdict[b.ToString()];
                Logging.Write("Pattern для числа " + b + "  на линии №" + (i + 1) + " = " + pattern);
                var match = Regex.Match(stroka, pattern);
                if (match.Success)
                {

                    if (Sovpadeniya.ContainsKey(b.ToString()))
                    {

                        Sovpadeniya[b.ToString()]++;
                    }
                    else
                    {
                        Sovpadeniya.Add(b.ToString(), 1);
                    }
                    LOG("Обнаружено совпадение для числа " + b + " в строке №" + (i + 1) + " Всего совпадений " + Sovpadeniya[b.ToString()]);
                }

            }
        }
        int MaxValue = Sovpadeniya.Values.Max();
        foreach (var kvp in Sovpadeniya)
        {
            if (kvp.Value == MaxValue)
            {
                LOG("Максимальное число совпадений у цифры [" + kvp.Key + "] это " + kvp.Value);
                digit = kvp.Key;
            }
        }
        return digit;
    }





    void sdagsdgsdg()
    {
        var pattern = "[_=(::)]";
        var line = "=";
        var matchCollection = System.Text.RegularExpressions.Regex.Matches(line, pattern);
        robotManager.Helpful.Var.SetVar("dbgOutput", matchCollection.Count);
    }

    public int RazdelitelCount(List<string> list) //кол-во разделяющих символов
    {
        //int chislo = 1;
        var pattern = "[_=:~]";
        foreach (string line in list)
        {
            int symbolcount = 2;
            /*if(CapchaLines[0].Contains("::"))
            {
                symbolcount = symbolcount * 2;
            }*/
            var matchCollection = Regex.Matches(line.Substring(line.Length - 2, symbolcount), pattern);
            return  matchCollection.Count;
            /*if (line.Substring(line.Length - 2, 2) != "__")
            {
                return 1;
            }*/
            //list[i].Substring((list[i].Length - maxlength - 1), maxlength);
        }
        Logging.Write("return 2");
        return 2;
    }

    public static Dictionary<string, int> Sovpadeniya = new Dictionary<string, int>();
    public void CheckLine(List<string> list, int linenumber, Dictionary<string,string> dictionary)
    {
        LOG("Проверяем строку "+ linenumber);
        var line = list[linenumber];
        
        foreach(var pattern in dictionary)
        {
            //string str = "You must wait 13 Minutes 53 Seconds.Before speaking again";
            //string pattern = @"\b\W\d+\WM";
            Regex regex = new Regex(pattern.Value);
            //var match = System.Text.RegularExpressions.Regex.Match(line, pattern.Key, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            LOG("Проверяем 1 строку паттерном " + pattern.Value + " regex.Matches(line).Count = " + regex.Matches(line).Count);
            foreach (Match match in regex.Matches(line))
            {
                if(Sovpadeniya.ContainsKey(pattern.Key))
                {
                    Sovpadeniya[pattern.Key]++;
                }
                else
                {
                    Sovpadeniya.Add(pattern.Key, 1);
                }    
                Logging.Write("В строке один найдено совпадение паттерна " + match.Groups[0].Value + " cоответствующее значению " + pattern.Key);
            }
        }
    }

    
    void testnewcap4()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""_8___88888______88____88888__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""888_88___88____888___88___88_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""_88_88___88__88__8____88888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""_88_88___88_88888888_88___88_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""888__88888_____888____88888__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_____________________________"")"); //6
    }
    
    void testnewcap42()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""__$$$_____$___________$$$$$__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""_$$______$$$__$$$$___$$___$$_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""$$$$$$$___$$_$$___$$_$$___$$_"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""$$___$$___$$__$$$$$$_$$___$$_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""_$$$$$___$$$_____$$___$$$$$__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_______________$$$___________"")"); //6
    }
    
    void testnewcap()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""888888___8888___888888____8__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""___8888_888888__88_______888_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""__8888______888_888888____88_"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""____888__8888______8888___88_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""888888__8888888_888888___888_"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_____________________________"")"); //6
    }
    void testnewcap3()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""____88___888888____888_____88888__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""___888______8888__88______88___88_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""_88__8_____8888__8888888___88888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""88888888_____888_88___88__88___88_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""___888___888888___88888____88888__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage("" _________________________________"")"); //6
    }
    void testnewcap6()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""888888___________888888____888____"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""88________8888______8888__88______"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""888888___88___88___8888__8888888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""___8888___888888_____888_88___88__"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""888888_______88__888888___88888___"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""___________888____________________"")"); //6
    }
    
    void testnewcap2()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""__888_____88888__8888888_888888__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""_88______88___88_____888____8888_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""8888888___88888_____888____8888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""88___88__88___88___888_______888_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""_88888____88888___888____888888__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage("" _________________________________"")"); //6
    }
    void testnewcap7()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""8888888__8______________88___"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""____888_888__8888______888___"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""___888___88_88___88__88__8___"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""__888____88__888888_88888888_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""_888____888_____88_____888___"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""______________888____________"")"); //6
    }
    void testnewcap8()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""_88888__8888888_888888____88888__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""88___88_____888_88_______88___88_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""_88888_____888__888888____88888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""88___88___888______8888__88___88_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""_88888___888____888888____88888__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_________________________________"")"); //6
    }
    void testnewcap9()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""________888888______88___8888888_"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""_8888______8888____888_______888_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""88___88___8888___88__8______888__"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""_888888_____888_88888888___888___"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""____88__888888_____888____888____"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""__888____________________________"")"); //6
    }
    void testnewcap10()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""888888____8__8888888__88888__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""88_______888_____888_88___88_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""888888____88____888__88___88_"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""___8888___88___888___88___88_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""888888___888__888_____88888__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_____________________________"")"); //6
    }
    void testnewcap11()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""999999____9__9999999__99999__"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""99_______999_____999_99___99_"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""999999____99____999__99___99_"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""___9999___99___999___99___99_"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""999999___999__999_____99999__"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_____________________________"")"); //6
    }
    void testnewcap12()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""??????====?==???????==?????=="")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""??=======???=====???=??===??="")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""??????====??====???==??===??="")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""===????===??===???===??===??="")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""??????===???==???=====?????=="")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""============================="")"); //6
    }
    void testnewcap13()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""%%%%%%====%==%%%%%%%==%%%%%=="")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""%%=======%%%=====%%%=%%===%%="")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""%%%%%%====%%====%%%==%%===%%="")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""===%%%%===%%===%%%===%%===%%="")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""%%%%%%===%%%==%%%=====%%%%%=="")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""============================="")"); //6
    }
    void testnewcap14()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""========%%%%%%%=========%%%%%%==="")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""=%%%%=======%%%==%%%%===%%======="")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""%%===%%====%%%==%%===%%=%%%%%%==="")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""=%%%%%%===%%%====%%%%%%====%%%%=="")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""====%%===%%%========%%==%%%%%%==="")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""==%%%=============%%%============"")"); //6

    }
    void testnewcap15()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""::::::::::::::::???????::::::::::::::::::??????::::::"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""::????::::::::::::::???::::????::::::??::::::::::::::"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""??::::::??::::::::???::::??::::::??::??????::::::"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""::??????::::::???::::::::??????::::::::????::::"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""::::::::??::::::???::::::::::::::::??::::??????::::::"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""::::???::::::::::::::::::::::::::???::::::::::::::::::::::::"")"); //6

    }
    
    void testnewcap5()
    {
        Lua.RunMacroText(@"/run SendSystemMessage(""_8888___888888___8__888888___"")"); //1
        Lua.RunMacroText(@"/run SendSystemMessage(""888888_____8888_888_88_______"")"); //2
        Lua.RunMacroText(@"/run SendSystemMessage(""____888___8888___88_888888___"")"); //3
        Lua.RunMacroText(@"/run SendSystemMessage(""_8888_______888__88____8888__"")"); //4
        Lua.RunMacroText(@"/run SendSystemMessage(""8888888_888888__888_888888___"")"); //5
        Lua.RunMacroText(@"/run SendSystemMessage(""_____________________________"")"); //6
    }
    public static void CopyLogCaptchaFailed()
    {
        string subpath = Application.StartupPath + @"\CapchaScreens\";

        // создание папки зоны
        if (!Directory.Exists(subpath))
        {
            Logging.Write("[ScreenShoot]: создание папки для скринов и логов непройденной капчи");
            Directory.CreateDirectory(subpath);
        }
        try
        {
            /* string foldername = "SentOrderLogs";
             if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\" + foldername + "\\"))
             {
                 Logging.Write("создание папки для хранения логов '" + System.Windows.Forms.Application.StartupPath + @"\SentOrderLogs\" + "'");
                 Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\" + foldername + "\\");
             }*/
            var logFilePath = Others.GetCurrentDirectory + "\\Logs\\" + Logging.NameCurrentLogFile();
            string logfilename = ObjectManager.Me.Name + "-CaptchaFailed-" +DateTime.Now.ToString("yyyy-MM-dd - HH-mm-ss");
            var logCopyDest = subpath + "\\" + logfilename + ".log.html";
            if (File.Exists(logCopyDest))
                File.Delete(logCopyDest);
            File.Copy(logFilePath, logCopyDest);
            byte[] bData = File.ReadAllBytes(logCopyDest);
            UploadToTelegram(bData, LetsGoldBotToken, "document", CapchaAlertChannel, logCopyDest, "CaptchafailedLog");
            Logging.Write("[Logging]" + ObjectManager.Me.Name + "-" + logfilename + " saved");
        }
        catch
        {
        }
    }
    public void Capcha14()
    {
        //Lua.RunMacroText(@"/run SendSystemMessage(""8888888888"")");
        /*CheckLine(CapchaLines, 0, FirstLinePutterns);
        CheckLine(CapchaLines, 1, SecondLinePutterns);
        CheckLine(CapchaLines, 2, MidPutterns);
        CheckLine(CapchaLines, 3, fourthlinePutterns);
        CheckLine(CapchaLines, 4, BottomPutterns);
        foreach (KeyValuePair<string, int> kvp in Sovpadeniya)
        {
            Logging.Write("Всего совпадений для чистла " + kvp.Key + " - " + kvp.Value);
        }*/
        bool g = true;
        /*var text = "our security system has detected suspicious activity on your character. please solve a simple puzzle to eliminate sanctions for your character.select the number written now in the chat. if you don't see the number, please check if you have enabled the display of system messages in the chat.";
        var IsCaptcha = System.Text.RegularExpressions.Regex.Match(text, @"(S(e|е)L(e|е)(с|c)(т|t) (т|t)(h|н)(e|е) NU(м|m)(b|в)(e|е)R WRI(т|t)(т|t)(e|е)N N(o|о)W IN (т|t)(h|н)(e|е) (с|c)(h|н)(a|а)(т|t))", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        robotManager.Helpful.Var.SetVar("dbgOutput", IsCaptcha.Success);*/
        
        var IsCaptcha = Regex.Match(GossipText, @"(S(e|е)L(e|е)(с|c)(т|t) (т|t)(h|н)(e|е) NU(м|m)(b|в)(e|е)R WRI(т|t)(т|t)(e|е)N N(o|о)W IN (т|t)(h|н)(e|е) (с|c)(h|н)(a|а)(т|t))", RegexOptions.IgnoreCase);
        
        if (Lua.LuaDoString<bool>("if GossipFrame:IsVisible() then return true end") && IsCaptcha.Success && g)
        {
            Logging.Write("Капча нового типа с числами");
            Logging.Write("1");
            var digit = CheckDigit(CapchaLines);
            Logging.Write("2");
            //var lastdigit = CheckDigit(CapchaLines, false, true);
            LOG("Ответ - " + digit);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToString() == digit)
                {
                    sendDiscordMessageCaptcha(ObjectManager.Me.Name + " Успешно решили новую капчу, ответ = " + GossipTitleButtonText(i));
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }

    

    
    public void CaptchaEventsChecker(LuaEventsId id, List<string> args)
    {
        if (id == (LuaEventsId)Enum.Parse(typeof(LuaEventsId), "CHAT_MSG_SYSTEM"))
        {
            //Thread.Sleep(500);
            if ((args[0].Contains("888") || args[0].Contains("$$") || args[0].Contains("999") || args[0].Contains("__") || args[0].Contains("==") || args[0].Contains("??") || args[0].Contains("%%") || args[0].Contains(":::::") || args[0].Contains("~~") || args[0].Contains("&&")))
            {
                //log("КНТ: " + args[0]);
                //robotManager.Products.Products.InPause = true;
                //var linetext = ObjectManager.Me.Name + ": " + args[0];
                var linetext = args[0];
                if (CapchaTimer.IsReady)
                {
                    CapchaLines.Clear();
                    Sovpadeniya.Clear();
                    CapchaTimer = new robotManager.Helpful.Timer(5000);
                    System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        LOG("CapchaTimer start " + CapchaTimer.TimeLeft());
                        linetext = "";
                        Thread.Sleep((int)CapchaTimer.TimeLeft());
                        Capcha14();
                        //TGSMDebug(TOKEN, "-1001641102456", linetext);
                    });

                }
                if (!CapchaTimer.IsReady)
                {
                    CapchaLines.Add(linetext.Replace("::","_"));
                    LOG("Добавили сторку " + args[0] + " в list CapchaLines");
                    print("Добавили сторку " + args[0] + " в list CapchaLines");
                }
                //var linetext = ObjectManager.Me.Name + ": " + args[0];

                //SendDiscordMessageSystemCaptcha("Капча нового типа " + args[0]);

                return;
            }
        }
    }
    public static List<string> CapchaLines = new List<string>();
    public static robotManager.Helpful.Timer CapchaTimer = new robotManager.Helpful.Timer();
    public static bool NoDigitsOnLastLineList(List<string> list)
    {
        return !list.LastOrDefault().Contains("8");
    }
    void Capcha7()
    {
        //Natasha read 5 books during the holidays, and Katya read 4 books. How many books did the children read together during the holidays
        //var GossipText = "Наташа прочитала за каникулы 5 книг, а Катя прочитала 4 книги. Сколько книг дети прочитали вместе за каникулы?";
        if (GossipText.Contains("книг") || GossipText.Contains("book"))
        {
            LOG("Natasha read 5 books during the holidays, and Katya read 4 books. How many books did the children read together during the holidays");
            print("наташа и книги");
            string[] words = GossipText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> tmp = new List<int>();
            for (int i = 0; i <= words.Length - 1; i++)
            {
                int outint = 0;
                bool haveint = System.Int32.TryParse(words[i], out outint);
                if (haveint)
                    tmp.Add(outint);
            }
            int solution = 0;
            if (GossipText.Contains("вместе") || GossipText.Contains("together"))
            {
                solution = tmp.Sum();
            }
            if (GossipText.Contains("меньше") || GossipText.Contains("fewer"))
            {
                solution = tmp[0] - tmp[1];
            }
            if (GossipText.Contains("больше") || GossipText.Contains("more"))
            {
                solution = tmp[0] + tmp[1];
            }
            string cifra = solution.ToString();
            //int cifraint = System.Int32.Parse(cifrachar.ToString());
            var cifrakvp = number.DicNumbers.FirstOrDefault(v => v.digit == cifra);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i).ToLower() != "" && (GossipTitleButtonText(i).ToLower() == cifrakvp.digit || GossipTitleButtonText(i).ToLower() == cifrakvp.english || GossipTitleButtonText(i).ToLower() == cifrakvp.russian))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }
    void Capcha8()
    {
        if (GossipText.Contains("how many classes are in world of warcraft: wrath of the lich king"))
        {
            LOG("how many classes are in world of warcraft: wrath of the lich king");
            print("how many classes");
            var DicNumber = number.DicNumbers.FirstOrDefault(v => v.digit == "10");
/*            if (number.DicNumbers.Count(n => GossipText.Contains(n.digit) || GossipText.Contains(n.russian) || GossipText.Contains(n.english)) > 0)
            {*/
                if(GossipText.Contains("letters"))
                {
                    LOG("Ответ: " + DicNumber.english);
                    print("Ответ: " + DicNumber.english);
                }
                
                if (GossipText.Contains("numbers"))
                {
                    LOG("Ответ: " + DicNumber.digit);
                    print("Ответ: " + DicNumber.digit);
                }
                //LOG("ButtonCount: " + ButtonCount);
                //print("ButtonCount: " + ButtonCount);
                for (int i = 1; i <= ButtonCount; i++)
                {
                    LOG("Проверяем кнопку " + i);
                    print("Проверяем кнопку " + i);
                    if (GossipText.Contains("letters"))
                    {
                        if (GossipTitleButtonText(i) != "" && (GossipTitleButtonText(i).ToLower().Contains(DicNumber.russian) || GossipTitleButtonText(i).ToLower().Contains(DicNumber.english)))
                        {
                            Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                            LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                            print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                            CloseGossip();
                        }
                    }
                    if (GossipText.Contains("numbers"))
                    {
                        if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToLower().Contains(DicNumber.digit))
                        {
                            Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                            LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                            print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                            CloseGossip();
                        }
                    }
                }
           // }
        }
    }
    void Capcha9()
    {
        if (GossipText.Contains("who is the developer"))
        {
            LOG("who is the developer of world of warcraft: wrath of the lich king?");
            print("who is the developer");
            string otvet = "Blizzard Entertainment";
            LOG("Ответ: " + otvet);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToLower().Contains(otvet.ToLower()))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }

        }
    }
    void Capcha10()
    {
        if (GossipText.Contains("warchief of the horde"))
        {
            LOG("Warchief of the Horde");
            print("Warchief of the Horde");
            string otvet = "Thrall";
            LOG("Ответ: " + otvet);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToLower().Contains(otvet.ToLower()))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }
    void Capcha11()
    {
        if (GossipText.Contains("what is the name of the flying city"))
        {
            LOG("what is the name of the flying city?");
            print("flying city");
            string otvet = "Dalaran";
            LOG("Ответ: " + otvet);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToLower().Contains(otvet.ToLower()))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }
    void Capcha12()
    {
        if (GossipText.Contains("symbol"))
        {
            LOG("What profession allows you to create symbols?");
            print("symbols");
            string otvet = "Inscription";
            LOG("Ответ: " + otvet);
            for (int i = 1; i <= ButtonCount; i++)
            {
                if (GossipTitleButtonText(i) != "" && GossipTitleButtonText(i).ToLower().Contains(otvet.ToLower()))
                {
                    Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", i));
                    LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(i), i));
                    CloseGossip();
                }
            }
        }
    }
    
    void Opros()
    {
        //var GossipText = "Наташа прочитала за каникулы 5 книг, а Катя прочитала 4 книги. Сколько книг дети прочитали вместе за каникулы?";
        if (GossipText.Contains("уважаемые игроки"))
        {
            var randomgossipnumber = Others.Random(1, ButtonCount);
            Lua.LuaDoString(string.Format("GossipTitleButton{0}:Click()", randomgossipnumber));
            LOG(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(randomgossipnumber), randomgossipnumber));
            print(String.Format("Жмем на кнопку GossipTitleButton{0}, в которой текст " + GossipTitleButtonText(randomgossipnumber), randomgossipnumber));
            CloseGossip();
        }
    }
}
    