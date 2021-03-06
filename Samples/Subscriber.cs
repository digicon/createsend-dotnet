﻿using System;
using System.Collections.Generic;
using System.Text;
using createsend_dotnet;

namespace Samples
{
    public class SubscriberSamples
    {
        //see API documentation on where to get this value
        private string listID = "your_list_id";

        void BasicAdd()
        {
            Subscriber subscriber = new Subscriber(listID);

            try
            {
                string newSubscriberID = subscriber.Add("test@notarealdomain.com", "Test Name", null, false);
                Console.WriteLine(newSubscriberID);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        void AddWithCustomFields()
        {
            Subscriber subscriber = new Subscriber(listID);

            try
            {
                List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();
                customFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value" });
                customFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value2" });

                string newSubscriberID = subscriber.Add("test@notarealdomain.com", "Test Name", customFields, false);
                Console.WriteLine(newSubscriberID);
            }
            catch (CreatesendException ex)
            {
                ErrorResult error = (ErrorResult)ex.Data["ErrorResult"];
                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }

        void BatchAdd()
        {
            Subscriber subscriber = new Subscriber(listID);

            List<SubscriberDetail> newSubscribers = new List<SubscriberDetail>();

            SubscriberDetail subscriber1 = new SubscriberDetail("test1@notarealdomain.com", "Test Person 1", new List<SubscriberCustomField>());
            subscriber1.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value" });
            subscriber1.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value2" });

            newSubscribers.Add(subscriber1);

            SubscriberDetail subscriber2 = new SubscriberDetail("test2@notarealdomain.com", "Test Person 2", new List<SubscriberCustomField>());
            subscriber2.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey", Value = "Value3" });
            subscriber2.CustomFields.Add(new SubscriberCustomField() { Key = "CustomFieldKey2", Value = "Value4" });

            newSubscribers.Add(subscriber2);

            try
            {
                BulkImportResults results = subscriber.Import(newSubscribers, true);
                Console.WriteLine(results.TotalNewSubscribers + " subscribers added");
                Console.WriteLine(results.TotalExistingSubscribers + " total subscribers in list");
            }
            catch (CreatesendException ex)
            {
                ErrorResult<BulkImportResults> error = (ErrorResult<BulkImportResults>)ex.Data["ErrorResult"];

                Console.WriteLine(error.Code);
                Console.WriteLine(error.Message);

                if (error.ResultData != null)
                {
                    //handle the returned data
                    BulkImportResults results = error.ResultData;

                    //success details are here as normal
                    Console.WriteLine(results.TotalNewSubscribers + " subscribers were still added");

                    //but we also have additional failure detail
                    foreach (ImportResult result in results.FailureDetails)
                    {
                        Console.WriteLine("Failed Address");
                        Console.WriteLine(result.Message + " - " + result.EmailAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                //handle some other failure
            }
        }
    }
}
