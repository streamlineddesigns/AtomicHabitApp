using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class SavedResultModelRegistry
    {
        /*
         * The Registry of every day's results
         * int : Dictionary keys are dateTime.day parsed as ints
         */
        private Dictionary<int, List<SavedResultModel>> DayRegistry = new Dictionary<int, List<SavedResultModel>>();

        /*
         * The Registry of every day's results
         * int : Dictionary keys are dateTime.month parsed as ints
         */
        private Dictionary<int, List<SavedResultModel>> MonthRegistry = new Dictionary<int, List<SavedResultModel>>();

        public string saveFilePath = Application.persistentDataPath + "/SavedResultModels.json";

        /*
         * Constructor
         */
        public SavedResultModelRegistry()
        {
            loadFromFile();
        }

        /*
         * Used to initialize the Registry from a save file
         */
        protected void loadFromFile()
        {
            string jsonString;  

            if (! File.Exists(saveFilePath))
            {
                return;
            }

            Debug.Log(saveFilePath);

            System.IO.StreamReader file = new System.IO.StreamReader(saveFilePath);  
            while((jsonString = file.ReadLine()) != null)  
            {  
                SavedResultModel SavedResultModel = JsonUtility.FromJson<SavedResultModel>(jsonString);
                addASpecificResult(SavedResultModel);
            }
            
            file.Close();
        }

        /*
         * this function can be used to add a specific result to a day 
         * @param SavedResultModel : the data you want to add
         */
        public void addASpecificResult(SavedResultModel result)
        {
            int dayKey = result.date;
            if (DayRegistry.ContainsKey(dayKey)) {
                DayRegistry[dayKey].Add(result);
            } else {
                DayRegistry[dayKey] = new List<SavedResultModel>();
                DayRegistry[dayKey].Add(result);
            }

            addToMonthlyRegsitry(result);
        }

        public void addToMonthlyRegsitry(SavedResultModel result)
        {
            //int monthKey = new DateTime(result.dateTime).Month;

            int monthKey = int.Parse(new DateTime(result.dateTime).ToString("yyyyMM"));//NEW

            if (MonthRegistry.ContainsKey(monthKey)) {
                MonthRegistry[monthKey].Add(result);
            } else {
                MonthRegistry[monthKey] = new List<SavedResultModel>();
                MonthRegistry[monthKey].Add(result);
            }
        }

        /*
         * this function can be used to find out if a day has any data 
         * @param DateTimeOffset date : the date you want to check
         * @return bool : if the day has any data
         */
        public bool doesDayHaveData(DateTimeOffset date)
        {
            int key = int.Parse(date.ToString("yyyyMMdd"));
            if (DayRegistry.ContainsKey(key)) {
                return true;
            }

            return false;
        }  

        /*
         * this function can be used to find out if a month has any data 
         * @param DateTimeOffset date : the date you want to check
         * @return bool : if the day has any data
         */
        public bool doesMonthHaveData(DateTimeOffset date)
        {
            //int monthKey = date.Month;

            int monthKey = int.Parse(date.ToString("yyyyMM"));//NEW

            if (MonthRegistry.ContainsKey(monthKey)) {
                return true;
            }

            return false;
        }  

        /*
         * if a day has any data, this function can be used to retrieve that data
         * @param DateTimeOffset date : the date you want to check for any data
         * @return List<SavedResultModel> : a list of the data for the specified day
         */
        public List<SavedResultModel> getAnEntireDaysResults(DateTimeOffset date)
        {
            int key = int.Parse(date.ToString("yyyyMMdd"));
            if (DayRegistry.ContainsKey(key)) {
                return DayRegistry[key];
            }

            return null;
        }

        /*
         * if a month has any data, this function can be used to retrieve that data
         * @param DateTimeOffset date : the date you want to check for any data
         * @return List<SavedResultModel> : a list of the data for the specified day
         */
        public List<SavedResultModel> getAnEntireMonthsResults(DateTimeOffset date)
        {
            //int monthKey = date.Month;
            int monthKey = int.Parse(date.ToString("yyyyMM"));//NEW

            if (MonthRegistry.ContainsKey(monthKey)) {
                return MonthRegistry[monthKey];
            }

            return null;
        }

        /*
         * if a day has any data, this function can be used to retrieve a specific SavedResultModel
         * @param DateTimeOffset date : the date you want to check
         * @return SavedResultModel : the data for the specified date and time
         */
        public SavedResultModel getASpecificResult(DateTimeOffset date)
        {
            int key = int.Parse(date.ToString("yyyyMMdd"));
            long dateTime = date.Ticks;
            if (DayRegistry.ContainsKey(key)) {
                for (int i = 0; i < DayRegistry[key].Count; i++) {
                    if (DayRegistry[key][i].dateTime == dateTime) {
                        return DayRegistry[key][i];
                    }
                }
            }

            return null;
        }
    }

}