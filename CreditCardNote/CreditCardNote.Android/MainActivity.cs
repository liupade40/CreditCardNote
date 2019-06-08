using System;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using Naxam.Controls.Platform.Droid;
using Android.Content;
using Android.Database;
using Android.Icu.Text;
using CreditCardNote.View;
using CreditCardNote.Infrastructrue.Common;
using CreditCardNote.SQLite.Model;
using System.Collections.Generic;
using CreditCardNote.SQLite.DAL;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CreditCardNote.Droid
{
    [Activity(Label = "信用卡笔记", Icon = "@drawable/ic_credit_card_white_48dp", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static int smsCount;
        private static BillDAL billDAL = new BillDAL();
        protected override void OnCreate(Bundle bundle)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            SetupBottomTabs();
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //MessageBroadcastReceiver MessageBroadcastReceiver = new MessageBroadcastReceiver();
            // RegisterReceiver(MessageBroadcastReceiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));

            LoadApplication(new App());

        }
        protected override void OnStart()
        {
            base.OnStart();
            var status = CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Sms);
            if (PermissionStatus.Granted == status.Result)
            {
                CreditCardDAL creditCardDAL = new CreditCardDAL();
                string BankPhone = creditCardDAL.FindAllBankPhone();
                SMSTimeDAL sMSTimeDAL = new SMSTimeDAL();
                string timeStamp = sMSTimeDAL.FindTimeStamp();
                GetAllMessage(BankPhone, timeStamp);
                AddMessage();
            }
            else
            {
                MessageList.NoSMSPermissionTip();
            }
            CreditCardList.AddCardGetSMS = GetAllMessage;
        }

        private void AddMessage()
        {
            if (MessageList.BillList.Count == 0)
            {
                BillDAL billDAL = new BillDAL();
                var data = billDAL.FindList(1, 10);
                foreach (var item in data)
                {
                    MessageList.BillList.Add(item);
                }
            }
        }
        private void GetAllMessage(string bankPhone, string timeStamp)
        {
            smsCount = 0;
            if (bankPhone != null)
            {
                var smsResolver = this.ContentResolver;
                var uri = Android.Provider.Telephony.Sms.Inbox.ContentUri;
                System.String[] projection = new System.String[] { "_id", "address", "person", "body", "date", "type" };
                ICursor cur = smsResolver.Query(uri, projection, "date>? and type=1 and address in (" + bankPhone + ")", new string[] { timeStamp }, "date desc");
                if (cur.MoveToFirst())
                {
                    System.String name;
                    System.String phoneNumber;
                    System.String smsbody;
                    System.String date;
                    int nameColumn = cur.GetColumnIndex("person");
                    int phoneNumberColumn = cur.GetColumnIndex("address");
                    int smsbodyColumn = cur.GetColumnIndex("body");
                    int dateColumn = cur.GetColumnIndex("date");
                    BillDAL billDal = new BillDAL();
                    do
                    {
                        name = cur.GetString(nameColumn);
                        phoneNumber = cur.GetString(phoneNumberColumn);
                        smsbody = cur.GetString(smsbodyColumn);
                        date = cur.GetString(dateColumn);
                        Analysis(smsbody, date);
                    } while (cur.MoveToNext());
                }
            }
        }
        public static void Analysis(string content, string date)
        {
            if ((content.Contains("尾号") || content.Contains("信用卡")) && content.Contains("消费人民币"))
            {
                string shortNumber = null;
                if (content.Contains("民生银行"))
                {
                    shortNumber = content.Substring(content.IndexOf("信用卡") + 3, 4);
                }
                else
                {
                    shortNumber = content.Substring(content.IndexOf("尾号") + 2, 4);
                }
                //Console.WriteLine("尾号：{0}", weihao);
                string xiaofei = content.Substring(content.IndexOf("消费人民币") + 5);
                decimal cost = isnumeric(xiaofei);
                string bank = BankName(content);
                if (bank != "")
                {
                    //Console.WriteLine("消费金额：{0}", cost);
                    //Console.WriteLine("银行:{0}", type(content));
                    var model = new Bill()
                    {
                        AddTime = TimeHelper.ConvertStringToDateTime(date),
                        CostDate = date,
                        ShortNumber = shortNumber,
                        BankType = bank,
                        Money = cost,
                        Content = content
                    };
                    var b = billDAL.Add(model);
                    if (smsCount <= 10 && b)
                    {

                        MessageList.BillList.Insert(0, model);
                        smsCount++;
                    }
                }
            }
            else
            {
                //  Console.WriteLine("短信消息没有包含的关键字");
            }
        }
        public static string BankName(string str)
        {
            foreach (var item in BankType.BankNameList)
            {
                if (str.Contains(item))
                {
                    if (item == "华夏信用卡")
                    {
                        return "华夏银行";
                    }
                    return item;
                }
            }
            return "";
        }
        public static decimal isnumeric(string str)
        {
            char[] ch = new char[str.Length];
            ch = str.ToCharArray();
            string number = "";
            for (int i = 0; i < str.Length; i++)
            {
                if ((ch[i] >= 48 && ch[i] <= 57) || ch[i] == 46 || ch[i] == 44)
                {
                    number += ch[i];
                }
                else
                {
                    break;
                }
            }
            return decimal.Parse(number);
        }
        //bool doubleBackToExitPressedOnce = false;
        //public override void OnBackPressed()
        //{
        //    if (doubleBackToExitPressedOnce)
        //    {
        //        base.OnBackPressed();
        //        Java.Lang.JavaSystem.Exit(0);
        //        return;
        //    }
        //    this.doubleBackToExitPressedOnce = true;
        //    Toast.MakeText(this, "再按一次退出", ToastLength.Long).Show();
        //    new Handler().PostDelayed(() =>
        //    {
        //        doubleBackToExitPressedOnce = false;
        //    }, 2000);
        //}
        void SetupBottomTabs()
        {
            var stateList = new ColorStateList(
                new int[][] {
                    new int[] { Android.Resource.Attribute.StateChecked
                    },
                    new int[] { Android.Resource.Attribute.StateEnabled
                    }
                },
                new int[] {
                    new Color(56, 173, 255), //Selected
                    Color.Gray //Normal
                });

            BottomTabbedRenderer.BackgroundColor = Color.White;
            BottomTabbedRenderer.FontSize = 12f;
            BottomTabbedRenderer.IconSize = 16;
            BottomTabbedRenderer.ItemTextColor = stateList;
            BottomTabbedRenderer.ItemIconTintList = stateList;
            BottomTabbedRenderer.Typeface = Typeface.CreateFromAsset(this.Assets, "architep.ttf");
            BottomTabbedRenderer.ItemBackgroundResource = Resource.Drawable.bnv_selector;
            BottomTabbedRenderer.ItemSpacing = 4;
            //BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(6);
            BottomTabbedRenderer.BottomBarHeight = 56;
            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;
            BottomTabbedRenderer.MenuItemIconSetter = (menuItem, iconSource, selected) =>
            {
                var resId = Resources.GetIdentifier(iconSource.File, "drawable", PackageName);

                menuItem.SetIcon(resId);
            };
        }
    }
}

