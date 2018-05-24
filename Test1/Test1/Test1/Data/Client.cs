using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test1.Model;

namespace Test1.Data
{
    public class Client
    {
        public User thisUser;
        public IService Service;
        public enum Result { Success, Exist, Network, Empty}

        public Client(IService service)
        {
            Service = service;
        }

        public IService GetService()
        {
            return Service;
        }

        public async Task<Result> RegisterNewUser(User user)
        {
            try
            {
                string results = await Service.InvokePost(Constants.registrationUrl, JsonConvert.SerializeObject(user));

                ReturnMessage rmessage = JsonConvert.DeserializeObject <ReturnMessage>(results);
                string success = string.Format("User {0} was created", user.username);
                if (rmessage.message == success)
                {
                    return Result.Success;
                }
                else
                {
                    return Result.Exist;
                }
            }
            catch (HttpRequestException)
            {
                return Result.Network;
            }
        }

        public async Task<bool> Login(User user)
        {
            try
            {
                string results = await Service.InvokePost(Constants.loginUrl, JsonConvert.SerializeObject(user));
                ReturnMessage checkUser = JsonConvert.DeserializeObject<ReturnMessage>(results);
                string checkmessage = string.Format("Logged in as {0}", user.username);
                if (checkUser.message == checkmessage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<Result> AddJob(Job newjob)
        {
            try
            {
                string results = await Service.InvokePost(Constants.addjobUrl, JsonConvert.SerializeObject(newjob));

                ReturnMessage rmessage = JsonConvert.DeserializeObject<ReturnMessage>(results);
                string success = string.Format("Job at location {0} was created", newjob.address);
                if (rmessage.message == success)
                {
                    return Result.Success;
                }
                else
                {
                    return Result.Exist;
                }
            }
            catch (HttpRequestException)
            {
                return Result.Network;
            }
        }

        public async Task<List<Job>> GetJobs()
        {
            try
            {
                string results = await Service.InvokeGet(Constants.getjobsUrl);
                return JsonConvert.DeserializeObject<List<Job>>(results);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
