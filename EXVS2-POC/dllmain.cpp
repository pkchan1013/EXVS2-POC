#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <string>

#include "GameHooks.h"
#include "JvsEmu.h"
#include "WindowedDxgi.h"
#include "ConfigReader.h"
#include "VirtualKeyMapping.h"

struct config_struct {
    bool Windowed;
} config;

config_struct ReadConfigs(cppsecrets::ConfigReader* p) {
    config_struct config;

    // parse the configuration file
    p->parseFile();

    // Define variables to store the value
    std::string windowed("");

    // Assign config variable
    p->getValue("Windowed", windowed);

    bool isWindowed = false;
    isWindowed = (windowed.compare("true") == 0);
    config.Windowed = isWindowed;

    return config;
}

jvs_key_bind ReadKeyMappings(cppsecrets::ConfigReader* p) {
    jvs_key_bind key_bind;

    // default values
    key_bind.Test = 'T';
    key_bind.Start = 'O';
    key_bind.Service = 'S';
    key_bind.Up = VK_UP;
    key_bind.Left = VK_LEFT;
    key_bind.Down = VK_DOWN;
    key_bind.Right = VK_RIGHT;
    key_bind.Button1 = 'Z';
    key_bind.Button2 = 'X';
    key_bind.Button3 = 'C';
    key_bind.Button4 = 'V';

    // parse key mapping config file
    p->parseFile("KeyMapping.ini");

    std::string keyMapPlaceholder("");

    // read the values and modify the key_bind variable in JvsEmu
    p->getValue("Test", keyMapPlaceholder);
    key_bind.Test = findKeyByValue(keyMapPlaceholder);
    p->getValue("Start", keyMapPlaceholder);
    key_bind.Start = findKeyByValue(keyMapPlaceholder);
    p->getValue("Service", keyMapPlaceholder);
    key_bind.Service = findKeyByValue(keyMapPlaceholder);
    p->getValue("Up", keyMapPlaceholder);
    key_bind.Up = findKeyByValue(keyMapPlaceholder);
    p->getValue("Left", keyMapPlaceholder);
    key_bind.Left = findKeyByValue(keyMapPlaceholder);
    p->getValue("Down", keyMapPlaceholder);
    key_bind.Down = findKeyByValue(keyMapPlaceholder);
    p->getValue("Right", keyMapPlaceholder);
    key_bind.Right = findKeyByValue(keyMapPlaceholder);
    p->getValue("Button1", keyMapPlaceholder);
    key_bind.Button1 = findKeyByValue(keyMapPlaceholder);
    p->getValue("Button2", keyMapPlaceholder);
    key_bind.Button2 = findKeyByValue(keyMapPlaceholder);
    p->getValue("Button3", keyMapPlaceholder);
    key_bind.Button3 = findKeyByValue(keyMapPlaceholder);
    p->getValue("Button4", keyMapPlaceholder);
    key_bind.Button4 = findKeyByValue(keyMapPlaceholder);

    return key_bind;
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    // Read config
    // Create object of the class ConfigReader
    cppsecrets::ConfigReader* p = cppsecrets::ConfigReader::getInstance();

    config_struct config = ReadConfigs(p);
    jvs_key_bind keyBind = ReadKeyMappings(p);

    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        InitializeHooks();
        InitializeJvs(keyBind);
        InitDXGIWindowHook(config.Windowed);
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
	
    return TRUE;
}
