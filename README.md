# Saving-System

This saving system allows you to save and load game data. The user has 4 slots to save different data.

1. Data save in JSON format which contains keys and values. You can use the encryption checkbox to hide the data in the file.

2. To add a new saving/reading format you have only to edit the Save and Load functions in the FileDataHandler script.  

3. The project saves data locally and to the cloud. You can choose whether to load data locally or from the cloud. 

4. Backward compatibility - All data are in the GameData script. The Gamedata constructor sets default values for data.
If you want to load data from JSON to a new format you have to add new serialize and deserialize functions to Load functions in FileDataHandler,
Next, you have to catch exceptions if the format is not found and load data if the format is found. 
When format is found data are loaded from the format file to our Gamedata object.
