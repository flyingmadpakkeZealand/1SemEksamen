using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.Data.Json;
using Windows.Storage;
using _1SemEksamen.Exceptions;
using _1SemEksamen.MainModel;
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
        Kvitteringer,
        Users,
        Admins,
        TestDictionary,
        TestList,
        TestObservableCollection
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
            jsonString = await Task.Run(() => JsonConvertMethod(objectToSave));

            await SerializeFiles(jsonString, saveFile, saveMode);
        }

        private static string JsonConvertMethod(object objectToSave)
        {
            return JsonConvert.SerializeObject(objectToSave);
        }

        private static async Task SerializeFiles(string jsonString, ProgramSaveFiles saveFile, SaveMode saveMode)
        {
            try
            {
                switch (saveMode)
                {
                    case SaveMode.NewEveryTime:
                        await NewEveryTimeMethod(jsonString, saveFile);
                        break;
                    case SaveMode.Continuous:
                        await ContinuousMethod(jsonString, saveFile);
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
                //MessageDialogHelper.Show("Your save file could not be found, have you forgot to save any data to it?\n" + fnfx.Message, "File not Found Exception");
                //return null;
                throw new FileNotSavedException("Your save file could not be found, have you forgot to save any data to it ?\n", fnfx);
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




        //New Methods.
        //Saving.
        public static async Task SaveCollectionWithPolymorphism(object collectionToSave, ProgramSaveFiles saveFile)
        {
            Type collectionType = collectionToSave.GetType();
            string collectionNameSpace = collectionType.Namespace;
            if (collectionNameSpace == "System.Collections.Generic" || collectionNameSpace == "System.Collections.ObjectModel")
            {
                bool containsKeyValuePairs = collectionType.GetProperty("Keys") != null;
                PropertyInfo countInfo = collectionType.GetProperty("Count");
                Nullable<int> collectionCount = countInfo.GetValue(collectionToSave) as Nullable<int>;
                if (collectionCount <= 0 || collectionCount == null)
                {
                    throw new CollectionTooSmallException("Please only save a collection with at least one element");
                }
                MethodInfo getEnumeratorInfo = collectionType.GetMethod("GetEnumerator");
                object collectionEnum = getEnumeratorInfo.Invoke(collectionToSave, null);
                List<object> results = await DisectCollectionEnum(collectionEnum, containsKeyValuePairs, collectionCount);
                List<Type> elementTypes = results[0] as List<Type>;
                List<int> elementsLength = results[1] as List<int>; //Tested with list and dictionary to this point and found it working as intended.
                string metaData = await PrepareMetaData(elementTypes, elementsLength);


                //NewEveryTimeImplementation
                StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(saveFile.ToString() + ".dat",
                    CreationCollisionOption.ReplaceExisting);
                string saveData = "";
                await Task.Run(() => { saveData = metaData + JsonConvert.SerializeObject(collectionToSave); });

                await FileIO.WriteTextAsync(localFile, saveData);
            }
            else
            {
                throw new WrongOrUnsupportedTypeException("Unsupported type. The implementation of this method doesn't support the collection type it received. If it is indeed a collection contact the developer, lol");
            }
        }

        #region PreparingMetaData

        private static async Task<List<object>> DisectCollectionEnum(object collectionEnumToDisect, bool isKeyValuePair, int? count)
        {
            List<object> disectedElements = new List<object>();
            List<int> collectionElementsLength = new List<int>();
            switch (isKeyValuePair)
            {
                case true:
                    {
                        List<object> keyValuePairs = await DisectNormalEnum(collectionEnumToDisect, count);
                        List<List<object>> results =
                            await DisectKeyValuePairEnum(keyValuePairs);
                        List<object> values = results[0];
                        List<object> keys = results[1];

                        await Task.Run(() =>
                        {
                            for (int i = 0; i < values.Count; i++)
                            {
                                collectionElementsLength.Add(JsonConvert.SerializeObject(values[i]).Length +
                                                             JsonConvert.SerializeObject(keys[i]).Length + 1);
                            }
                        });

                        disectedElements = values;
                    }
                    break;
                case false:
                    {
                        List<object> values = await DisectNormalEnum(collectionEnumToDisect, count);

                        await Task.Run(() =>
                        {
                            for (int i = 0; i < values.Count; i++)
                            {
                                collectionElementsLength.Add(JsonConvert.SerializeObject(values[i]).Length);
                            }
                        });

                        disectedElements = values;
                    }
                    break;
            }
            List<Type> collectionOfElementTypes = new List<Type>();
            await Task.Run(() =>
            {
                foreach (var disectedElement in disectedElements)
                {
                    collectionOfElementTypes.Add(disectedElement.GetType());
                }
            });

            List<object> elementTypesAndLength = new List<object>();
            elementTypesAndLength.Add(collectionOfElementTypes);
            elementTypesAndLength.Add(collectionElementsLength);

            return elementTypesAndLength;
        }

        private static async Task<List<object>> DisectNormalEnum(object collectionEnumToDisect, int? count)
        {
            List<object> listOfCollectionElements = new List<object>();
            Type collectionEnumType = collectionEnumToDisect.GetType();
            MethodInfo moveNextInfo = collectionEnumType.GetMethod("MoveNext");
            MethodInfo disposeInfo = collectionEnumType.GetMethod("Dispose");
            PropertyInfo currentPropertyInfo = collectionEnumType.GetProperty("Current");
            await Task.Run(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    moveNextInfo.Invoke(collectionEnumToDisect, null);
                    object element = currentPropertyInfo.GetValue(collectionEnumToDisect);
                    listOfCollectionElements.Add(element);
                }
            });

            disposeInfo.Invoke(collectionEnumToDisect, null);
            return listOfCollectionElements;
        }

        private static async Task<List<List<object>>> DisectKeyValuePairEnum(List<object> listOfCollectionKeyValueElements)
        {
            PropertyInfo valuePropertyInfo = listOfCollectionKeyValueElements[0].GetType().GetProperty("Value");
            PropertyInfo keyPropertyInfo = listOfCollectionKeyValueElements[0].GetType().GetProperty("Key");
            List<object> listOfCollectionElements = new List<object>();
            List<object> listOfCollectionKeys = new List<object>();

            await Task.Run(() =>
            {
                foreach (var keyValueElement in listOfCollectionKeyValueElements)
                {
                    listOfCollectionElements.Add(valuePropertyInfo.GetValue(keyValueElement));
                    listOfCollectionKeys.Add(keyPropertyInfo.GetValue(keyValueElement));
                }
            });

            List<List<object>> results = new List<List<object>>();
            results.Add(listOfCollectionElements);
            results.Add(listOfCollectionKeys);
            return results;
        }

        private static async Task<string> PrepareMetaData(List<Type> elementTypes, List<int> elementsLength)
        {
            StringBuilder metaData = new StringBuilder();
            await Task.Run(() =>
            {
                string characters = JsonConvert.SerializeObject(elementTypes);
                int totalMetaDataLength = characters.Length;
                int typeMetaDataLength = characters.Length;
                metaData.Append(characters);
                characters = JsonConvert.SerializeObject(elementsLength);
                totalMetaDataLength = totalMetaDataLength + characters.Length;
                int lengthMetaDataLength = characters.Length;
                metaData.Append(characters);
                metaData.Insert(0, totalMetaDataLength.ToString() + ";" + typeMetaDataLength.ToString() + ";" + lengthMetaDataLength.ToString() + ";");
                //IMPORtANT - totalMetaDataLength does not include the numbers at the beginning. Those are MetaMetaData :P
            });
            return metaData.ToString();
        }

        #endregion

        //Loading.
        public static async Task<object> LoadCollectionWithPolymorphism(ProgramSaveFiles saveFile, Type mainCollectionType, Type[] subCollectionTypes)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(saveFile.ToString() + ".dat");
            string saveData = await FileIO.ReadTextAsync(localFile);
            List<string> seperatedMetaData = SeperateMetaData(saveData); //Order: type data, elementsLength data, pureData(the collection itself).
            string pureData = seperatedMetaData[2]; //BreakPoint.

            List<Type> collectionElementTypes = new List<Type>();
            List<int> collectionElementsLength = new List<int>();

            List<Task> loadMetaDataListsTask = new List<Task>();
            loadMetaDataListsTask.Add(Task.Run(() =>
            {
                collectionElementTypes =
                    JsonConvert.DeserializeObject(seperatedMetaData[0], typeof(List<Type>)) as List<Type>;
            }));
            loadMetaDataListsTask.Add(Task.Run(() =>
            {
                collectionElementsLength =
                    JsonConvert.DeserializeObject(seperatedMetaData[1], typeof(List<int>)) as List<int>;
            }));

            await Task.WhenAll(loadMetaDataListsTask);

            List<int> combinedElementsLength = new List<int>();
            combinedElementsLength.Add(collectionElementsLength[0]);
            List<int> TypesKeys = new List<int>() { 0 };

            int listIndex = 0;
            for (int i = 1; i < collectionElementTypes.Count; i++)
            {
                if (collectionElementTypes[i] == collectionElementTypes[i - 1])
                {
                    combinedElementsLength[listIndex] += collectionElementsLength[i] + 1;
                }
                else
                {
                    TypesKeys.Add(i);
                    combinedElementsLength.Add(collectionElementsLength[i]);
                    listIndex++;
                }
            }

            string pureDataOpeningCharacter = pureData.Substring(0, 1); //Break point.
            pureData = pureData.Remove(0, 1);
            string pureDataClosingCharacter = pureData.Substring(pureData.Length - 1, 1);
            //pureData is now open.

            Dictionary<Type, string> typeSeparatedDataStrings = new Dictionary<Type, string>();
            //typeSeparatedDataStrings.Add(collectionElementTypes[uniqueTypesKeys[0]],pureData.Substring(0,combinedElementsLength[0]));
            //pureData = pureData.Remove(0, combinedElementsLength[0]);

            int index = 0;
            foreach (int typeKey in TypesKeys) //Break point.
            {
                if (typeSeparatedDataStrings.ContainsKey(collectionElementTypes[typeKey]))
                {
                    typeSeparatedDataStrings[collectionElementTypes[typeKey]] += "," +
                        pureData.Substring(0, combinedElementsLength[index]);
                    pureData = pureData.Remove(0, combinedElementsLength[index] + 1);
                }
                else
                {
                    typeSeparatedDataStrings.Add(collectionElementTypes[typeKey], pureData.Substring(0, combinedElementsLength[index]));
                    pureData = pureData.Remove(0, combinedElementsLength[index] + 1);
                }

                index++;
            }


            List<object> deserializedSubCollections = new List<object>(); //Break point.
            object mainCollection = null;
            List<Type> subCollectionParamTypes = new List<Type>();
            await Task.Run(() =>
            {
                foreach (Type subCollectionType in subCollectionTypes)
                {
                    Type actualType = GetGenericParamFromCollection(subCollectionType);
                    if (typeSeparatedDataStrings.ContainsKey(actualType))
                    {
                        subCollectionParamTypes.Add(actualType);
                        string subCollectionDataString =
                            pureDataOpeningCharacter + typeSeparatedDataStrings[actualType] +
                            pureDataClosingCharacter;
                        deserializedSubCollections.Add(JsonConvert.DeserializeObject(subCollectionDataString,
                            subCollectionType));
                    }
                }

                string mainCollectionString = pureDataOpeningCharacter + pureDataClosingCharacter;
                    mainCollection = JsonConvert.DeserializeObject(mainCollectionString, mainCollectionType);
                
            });

            mainCollection = await AddToMainCollection(mainCollection, deserializedSubCollections, TypesKeys, collectionElementTypes,
                subCollectionParamTypes);

            return mainCollection; //;D
            //Dictionary<Type,List<int>> typeSeperatedDictionary = new Dictionary<Type, List<int>>();
            //typeSeperatedDictionary.Add(collectionElementTypes[0],new List<int>(){collectionElementslength[0]});

            //int listCount = 0;
            //for (int i = 1; i < collectionElementTypes.Count; i++)
            //{
            //    if (collectionElementTypes[i]==collectionElementTypes[i-1])
            //    {
            //        typeSeperatedDictionary[collectionElementTypes[i]][listCount] += collectionElementslength[i];//Method that does this, then starts a new method like this for new types? Then have them jump back and forth.
            //    }
            //}
        } //Break point.

        #region Methods to handle meta-data and to put actual into a collection ... Some methods are better than others :P

        private static List<string> SeperateMetaData(string saveData)
        {
            string[] indexStrings = saveData.Split(';', 4); //First three are indexes, the fourth is all the data itself.
            int typeMetaDataLength = Convert.ToInt32(indexStrings[1]);
            int elementsLengthMetaDataLength = Convert.ToInt32(indexStrings[2]);
            string typeMetaDataString = indexStrings[3].Substring(0, typeMetaDataLength);
            string elementsLengthMetaDataString = indexStrings[3].Substring(typeMetaDataLength, elementsLengthMetaDataLength);

            string pureDataString = indexStrings[3].Remove(0, typeMetaDataLength + elementsLengthMetaDataLength);


            List<string> seperatedMetaData = new List<string>() { typeMetaDataString, elementsLengthMetaDataString, pureDataString };

            return seperatedMetaData;
        }

        private static Type GetGenericParamFromCollection(Type theCollectionType)
        {
            if (theCollectionType.GetProperty("Keys") != null)
            {
                Type[] genericTypeArguements = theCollectionType.GenericTypeArguments;
                return genericTypeArguements[1];
            }
            else
            {
                Type[] genericTypeArguements = theCollectionType.GenericTypeArguments;
                return genericTypeArguements[0];
            }
        }

        //Method works, but could be more DRY.
        private static async Task<object> AddToMainCollection(object mainCollection, List<object> subCollections, List<int> typesKeys, List<Type> collectionElementTypes, List<Type> subCollectionParamTypes)
        {
            Type mainCollectionType = mainCollection.GetType(); //Break point.
            MethodInfo addInfo = mainCollectionType.GetMethod("Add");
            //MethodInfo getEnumeratorInfo = mainCollectionType.GetMethod("GetEnumerator");
            //PropertyInfo countPropertyInfo = mainCollectionType.GetProperty("Count");
            if (mainCollectionType.GetProperty("Keys") != null)
            {
                List<List<List<object>>> allResults = new List<List<List<object>>>();
                foreach (object subCollection in subCollections)
                {
                    Type subCollectionType = subCollection.GetType();
                    MethodInfo getEnumeratorInfo = subCollectionType.GetMethod("GetEnumerator");
                    PropertyInfo countPropertyInfo = subCollectionType.GetProperty("Count");

                    object collectionEnumToDisect = getEnumeratorInfo.Invoke(subCollection, null);

                    Nullable<int> count = countPropertyInfo.GetValue(subCollection) as Nullable<int>;
                    List<object> keyValuePairs = await DisectNormalEnum(collectionEnumToDisect, count);
                    List<List<object>> results =
                        await DisectKeyValuePairEnum(keyValuePairs);
                    allResults.Add(results);
                }

                int amount = 0;
                Type type = null;
                int collectionToTakeFrom = 0;
                List<object> values = null;
                List<object> keys = null;

                int counter = 0;
                int i = 0; //Break point.
                for (i = 1; i < typesKeys.Count; i++)
                {
                    amount = typesKeys[i] - typesKeys[i - 1];
                    type = collectionElementTypes[typesKeys[i - 1]];
                    collectionToTakeFrom = subCollectionParamTypes.IndexOf(type);
                    values = allResults[collectionToTakeFrom][0];
                    keys = allResults[collectionToTakeFrom][1];
                    for (int j = 0; j < amount; j++)
                    {
                        object[] keyValuePair = {keys[0],values[0]};
                        addInfo.Invoke(mainCollection, keyValuePair); //Don't panic, might go wrong - Didn't go wrong :D
                        values.RemoveAt(0);
                        keys.RemoveAt(0);
                        counter++;
                    }
                }

                amount = collectionElementTypes.Count - counter; //Break point.
                
                type = collectionElementTypes[collectionElementTypes.Count - 1];
                collectionToTakeFrom = subCollectionParamTypes.IndexOf(type);
                values = allResults[collectionToTakeFrom][0];
                keys = allResults[collectionToTakeFrom][1];
                for (int j = 0; j < amount; j++)
                {
                    object[] keyValuePair = { keys[0], values[0] };
                    addInfo.Invoke(mainCollection, keyValuePair); //Don't panic, might go wrong - Didn't go wrong :D
                    values.RemoveAt(0);
                    keys.RemoveAt(0);
                }

                return mainCollection;
            }
            else
            {
                List<List<object>> allResults = new List<List<object>>();
                foreach (object subCollection in subCollections)
                {
                    Type subCollectionType = subCollection.GetType();
                    MethodInfo getEnumeratorInfo = subCollectionType.GetMethod("GetEnumerator");
                    PropertyInfo countPropertyInfo = subCollectionType.GetProperty("Count");

                    object collectionEnumToDisect = getEnumeratorInfo.Invoke(subCollection, null);

                    Nullable<int> count = countPropertyInfo.GetValue(subCollection) as Nullable<int>;
                    List<object> currentValues = await DisectNormalEnum(collectionEnumToDisect, count);
                    allResults.Add(currentValues);
                }

                int amount = 0;
                Type type = null;
                int collectionToTakeFrom = 0;
                List<object> values = null;

                int counter = 0;
                int i = 0; //Break point.
                for (i = 1; i < typesKeys.Count; i++)
                {
                    amount = typesKeys[i] - typesKeys[i - 1];
                    type = collectionElementTypes[typesKeys[i - 1]];
                    collectionToTakeFrom = subCollectionParamTypes.IndexOf(type);
                    values = allResults[collectionToTakeFrom];
                    for (int j = 0; j < amount; j++)
                    {
                        object[] value = { values[0] };
                        addInfo.Invoke(mainCollection, value); //Don't panic, might go wrong - Didn't go wrong :D
                        values.RemoveAt(0);
                        counter++;
                    }
                }

                amount = collectionElementTypes.Count - counter; //Break point.
                
                type = collectionElementTypes[collectionElementTypes.Count - 1];
                collectionToTakeFrom = subCollectionParamTypes.IndexOf(type);
                values = allResults[collectionToTakeFrom];
                for (int j = 0; j < amount; j++)
                {
                    object[] value = { values[0] };
                    addInfo.Invoke(mainCollection, value); //Don't panic, might go wrong - Didn't go wrong :D
                    values.RemoveAt(0);
                }

                return mainCollection;

            }
        }

        #endregion

    }

    #endregion
}
