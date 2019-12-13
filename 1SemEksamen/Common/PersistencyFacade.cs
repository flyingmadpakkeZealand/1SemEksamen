using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.Data.Json;
using Windows.Storage;
using _1SemEksamen.Tristan.Model;
using Newtonsoft.Json;

namespace _1SemEksamen.Common
{
    //To add a new save file type its name like shown in the enumerator below.
    public enum ProgramSaveFiles
    {
        Example,
        Tickets,
        Rundvisninger,
        Kvitteringer
    }

    #region PersistencyFacade Code

    public enum SaveMode
    {
        NewEveryTime,
        Continuous
    }

    public static class PersistencyFacade
    {
        private const string fileIndex = "Index";

        //Saving.

        /// <summary>
        /// A static method for saving a object asynchronously as a JSON file. The object can be of any kind, including lists of objects. 
        /// </summary>
        /// <param name="objectToSave">The object that is saved. Examples could include, int list&lt;int&gt;, string etc.</param>
        /// <param name="saveFile">The name of the save file. Currently this is restricted to an enumerator type to reduce risk.
        /// If you want to save to a new file, you should first add another value to the ProgramSaveFile enum under PersistencyFacade. </param>
        /// <param name="saveMode"> NewEveryTime: Each time the method is used to save to a file, that file is overwritten with the new content, if it already exist.
        /// Continuous: If the save file already exist the new content is added to file instead of overwriting it.
        /// You should only continuously save the same kind of data so that loading it is easy. Example, if you save an int, don't save a string to the same file, or a list of ints.'</param>
        /// <returns>Task</returns>
        public static async Task SaveObjectsAsync(object objectToSave, ProgramSaveFiles saveFile, SaveMode saveMode)
        {
            string jsonString = "";
            await Task.Run(() =>
            {
                jsonString = JsonConvert.SerializeObject(objectToSave);
            });
            await SerializeFiles(jsonString, saveFile, saveMode);
        }

        private static async Task SerializeFiles(string jsonString, ProgramSaveFiles saveFile, SaveMode saveMode)
        {
            try
            {
                switch (saveMode)
                {
                    case SaveMode.NewEveryTime : await NewEveryTimeMethod(jsonString, saveFile);
                        break;
                    case SaveMode.Continuous : await ContinuousMethod(jsonString, saveFile);
                        break;
                }
            }
            catch (IOException iox)
            {
                //Implement Exception.
                MessageDialogHelper.Show("There was a problem accessing your files:\n" + iox.Message, "File In/Out Exception");
            }
        }

        private static async Task NewEveryTimeMethod(string jsonString, ProgramSaveFiles saveFile)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                saveFile.ToString() + ".dat",
                CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, jsonString);
            
        }

        private static async Task ContinuousMethod(string jsonString, ProgramSaveFiles saveFile)
        {
            await SaveIndexContinuousHelper(jsonString, saveFile);
            await WriteToFileContinuousHelper(jsonString, saveFile.ToString());
        }

        private static async Task WriteToFileContinuousHelper(string jsonString, string saveFile)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                saveFile + ".dat",
                CreationCollisionOption.OpenIfExists);
            await FileIO.AppendTextAsync(localFile, jsonString);
        }

        private static async Task SaveIndexContinuousHelper(string jsonString, ProgramSaveFiles saveFile)
        {
            string jsonStringLength = jsonString.Length.ToString();
            jsonStringLength = jsonStringLength + ";";
            string saveFileIndex = saveFile.ToString() + fileIndex;
            await WriteToFileContinuousHelper(jsonStringLength, saveFileIndex);
        }

        //Loading.

        /// <summary>
        /// A static method for asynchronously loading objects from a JSON file. This includes just a singular object.
        /// </summary>
        /// <param name="saveFile">The save file that will be loaded.</param>
        /// <param name="objectType">The type of object that should be loaded. If using continuous mode to save, the "outer" type should always be of type List,
        /// even if saving a List.
        /// Example, if saving a Ticket object continuously, objectType is List&lt;Ticket&gt;, if saving a List&lt;Ticket&gt; continuously, objectType is List&lt;List&lt;Ticket&gt;&gt;
        /// Use the "typeof" keyword to get the type.</param>
        /// <returns>Object. If continuous mode was used to save, the returned object will always be a list. Use typecast declare what kind of list is returned.
        /// Example, if loading Ticket object(s) continuously typecast the return value of the method to (List&lt;Ticket&gt;)</returns>
        public static async Task<object> LoadObjectsAsync(ProgramSaveFiles saveFile, Type objectType)
        {
            StorageFile localFile = null;
            StorageFile localFileIndex = null;
            List<int> savedElementsLengthList = null;
            try
            {
                string filePathNormal = saveFile.ToString() + ".dat";
                localFile = await GetStorageFile(filePathNormal);
            }
            catch (FileNotFoundException fnfx)
            {
                MessageDialogHelper.Show("Your save file could not be found, have you forgot to save any data to it?\n" + fnfx.Message, "File not Found Exception");
                return null;
            }
            try
            {
                string filePathIndex = saveFile.ToString() + fileIndex + ".dat";
                localFileIndex = await GetStorageFile(filePathIndex);
                savedElementsLengthList = await LoadIndexFile(localFileIndex);
            }
            catch (FileNotFoundException fnfx)
            {
                object loadedObject = await LoadNormalFile(localFile, objectType);
                return loadedObject;
            }

            string jsonString = await FileIO.ReadTextAsync(localFile);

            object loadedObjects = null;
            StringBuilder loadedJsonObjectsAsList = new StringBuilder("[");


            try
            {
                await Task.Run(() =>
                {
                    foreach (int ElementLength in savedElementsLengthList)
                    {
                        string jsonObjectString = jsonString.Substring(0, ElementLength);
                        jsonString = jsonString.Remove(0, ElementLength);
                        if (jsonString.Length > 0)
                        {
                            loadedJsonObjectsAsList.Append(jsonObjectString + ",");
                        }
                        else
                        {
                            loadedJsonObjectsAsList.Append(jsonObjectString);
                        }
                    }

                    loadedJsonObjectsAsList.Append(']');

                    loadedObjects = JsonConvert.DeserializeObject(loadedJsonObjectsAsList.ToString(), objectType);
                });
            }
            catch (Exception e)
            {
                throw;
                //Implement possible exceptions, one would be if a user used both SaveModes with the same file. The file and the indexfile would conflict, this error should probably just tell the user to only use one SaveMethod, as using both for the same file makes no sense.
            }

            return loadedObjects;
        }

        private static async Task<StorageFile> GetStorageFile(string filePath)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(filePath);
            return localFile;
        }

        private static async Task<object> LoadNormalFile(StorageFile localFile, Type objectType)
        {
            string jsonString = await FileIO.ReadTextAsync(localFile);
            object loadedObject = null;
            await Task.Run(() =>
            {
                loadedObject = JsonConvert.DeserializeObject(jsonString, objectType);
            });
            return loadedObject;
        }

        private static async Task<List<int>> LoadIndexFile(StorageFile localFile)
        {
            string jsonIndexString = await FileIO.ReadTextAsync(localFile);
            List<int> savedElementsList = new List<int>();

            await Task.Run(() =>
            {
                string[] savedElementsStringArray = jsonIndexString.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (string element in savedElementsStringArray)
                {
                    savedElementsList.Add(Convert.ToInt32(element));
                }
            });

            return savedElementsList;
        }
    }

    #endregion
}
