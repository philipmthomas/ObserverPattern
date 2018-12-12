using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    // register to website.
    // send notifications to all users currently logged into website that
    // a new user just registered
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Site site = new Site();

            site.Register(new User { Id = 1, Email = "phthomas@me.com", UserName = "LucidCoder" });
            site.Register(new User { Id = 2, Email = "philip.m.thomas@me.com", UserName = "pmtXYZ" });
            site.Register(new User { Id = 3, Email = "pthomas357@hotmail.com", UserName = "FeralRanchu" });

            site.CreateNewTopic("Inclusivity in the Workplace.");

            site.Unregister(site.Users.Where(x => x.Id == 2).FirstOrDefault());

            site.CreateNewTopic("Inclusivity in the Workplace, version 2.");
        }
    }

    public class Site : SiteObservable
    {
        public IList<Observer> Users { get; set; }

        public Site()
        {
            Users = new List<Observer>();

            foreach (var user in Users) Register(user);
        }

        public override void Register(Observer observer) => Users.Add(observer);

        public override void Unregister(Observer observer) => Users.RemoveAt(Users.IndexOf(observer));

        public override void Notify()
        {
            foreach (var user in Users) user.Notify(TopicName);
        }

        private string TopicName { get; set; }
        public void CreateNewTopic(string topicName)
        {
            TopicName = topicName;

            Notify();
        }
    }

    public abstract class SiteObservable
    {
        public abstract void Register(Observer observer);
        public abstract void Unregister(Observer observer);
        public abstract void Notify();
    }

    public abstract class Observer
    {
        public int Id { get; set; }
        public abstract void Notify(string value);
    }

    public class User : Observer
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        public override void Notify(string topic)
        {
            Console.WriteLine($"{UserName} received {topic} at {DateTime.Now}");
        }
    }
}