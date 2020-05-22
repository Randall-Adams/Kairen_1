--[[ Meta-Data
Version: 1.0
]]--
--Code By Robert Randazzio
--Help By Dustin Faxon, Daniel Wallace and internet searches
--Testing By Jaramiah Johnson and Daniel Wallace
print("SCRIPT STARTED")
output_line_number = 0 -- do not change
output_file_lines = { }
output_file_lines[0] = "ll5"
--above can equal a number of lines, or a number of logic loops, 
-- ex: "ll200", for 200 logic loops, or "700" for 700 lines.
dofile("../EQOA/luas/File Paths.lua")
dofile(Folder_LUA_Modules .. "FAPI/FAPI.lua")

-- Temporary, Unconverted Global Script Variables Below
Thisbox = "Box1"
    value_Triangle = readBytes("[pcsx2-r3878.exe+0040239C]+974")
    value_Square = readBytes("[pcsx2-r3878.exe+0040239C]+975")
    value_X = readBytes("[pcsx2-r3878.exe+0040239C]+976")
    value_Circle = readBytes("[pcsx2-r3878.exe+0040239C]+977")
    --value_L1 = readBytes("[pcsx2-r3878.exe+0040239C]+97C")
    value_L2 = readBytes("[pcsx2-r3878.exe+0040239C]+97D")
    --value_L3 = readBytes("[pcsx2-r3878.exe+0040239C]+982")
    --value_R1 = readBytes("[pcsx2-r3878.exe+0040239C]+97E")
    value_R2 = readBytes("[pcsx2-r3878.exe+0040239C]+97F")
    --value_R3 = readBytes("[pcsx2-r3878.exe+0040239C]+983")
    --value_Up = readBytes("[pcsx2-r3878.exe+]+")
    --value_Right = readBytes("[pcsx2-r3878.exe+]+")
    --value_Down = readBytes("[pcsx2-r3878.exe+]+")
    --value_Left = readBytes("[pcsx2-r3878.exe+]+")
    value_Start = readBytes("[pcsx2-r3878.exe+0040239C]+980")
    value_Select = readBytes("[pcsx2-r3878.exe+0040239C]+981")

--Global Script Option Variables Here
--Function Options
doUpdateValues = false -- Updates Core Script Values to Equal the Game's
doUpdateAreas = false -- Spawn NPCs
doOutputLocation = false -- Output your location to console
doOutsideCommands = false -- Outside Commands
--Variable Options
doCOOutput = true -- Console Output

-- Non-Option Variables
LogicLoopsStarted = 0
LogicLoopsEnded = 0
NumberofGhosts = 0

function ProcessOutsideCommands()
    _sender = "ProcessOutsideCommands"
    co(_sender, "co_enter", "")
    _filepath = Folder_Temp .. "Outside Command" .. Extension_Outside_Command
    if File_Exists(_filepath) == true then
        co(_sender, "co_debugoutput", "File Exists: " .. _filepath)
        _file = io.open(_filepath, "r+");
        _outsideCommand = IO_ReadNextLine(_file, "%-%-")        
        if _outsideCommand ~= nil then -- Nothing
            co(_sender, "co_debugoutput", "_outsideCommand == " .. _outsideCommand)
            
            
            if _outsideCommand == "Spawn_NPC" then -- Spawn NPC
                _npc = IO_ReadNextLine(_file, "%-%-")
                while _npc ~= nil do
                    Spawn_NPC(_npc)
                    _npc = IO_ReadNextLine(_file, "%-%-")
                end
                
                
            elseif _outsideCommand == "Spawn_NPCs_By_Location" then -- Spawn Area
                _location = IO_ReadNextLine(_file, "%-%-")
                Spawn_NPCs_By_Location(_location)
                
                
            elseif _outsideCommand == "Spawn Wall Marker" then -- Spawn Wall Marker
                _number = IO_ReadNextLine(_file, "%-%-")
                _x = IO_ReadNextLine(_file, "%-%-")
                _y = IO_ReadNextLine(_file, "%-%-")
                _z = IO_ReadNextLine(_file, "%-%-")
                --_facing = IO_ReadNextLine(_file, "%-%-")
                NPC_Maker.SpawnWallMarker(_number, _x, _y, _z)--, _facing)
                
                
            elseif _outsideCommand == "AddOnlinePlayer" then -- Add Online Player
                _onlinePlayer = IO_ReadNextLine(_file, "%-%-")
                while _onlinePlayer ~= nil do
                    Online.AddOnlinePlayer(_onlinePlayer)
                    _onlinePlayer = IO_ReadNextLine(_file, "%-%-")
                end
                
            elseif _outsideCommand == "" then -- Blank
                co(_sender, "co_debugoutput", "_outsideCommand Is blank")
                
                
            end
        end
        _file:close();
        if File_Exists(_filepath) then os.remove(_filepath) end
    else
        --co(_sender, "co_debugoutput", "Notice: File Does Not Exist: " .. _filepath)
        --co(_sender, "co_debugoutput", "No Outside Command to Follow.")
    end
    co(_sender, "co_exit", "")
end

-- Dual Box Functions
function OutputDBData()
    local _sender = "OutputDBData"
    co(_sender, "co_enter", "")
    local file_name = ""
    if Thisbox == "Box1" then
        file_name = "Box1"
    else
        file_name = "Box2"
    end
    _filepath = Folder_DBData .. file_name .. Extension_DBData
    if File_Exists(_filepath) == true then
        --co(_sender, "co_debugoutput", "File Location: " .. _filepath)
        file = io.open(_filepath,"w+");
        
        x_output = My["my_X"] -- = readInteger("[pcsx2-r3878.exe+0040239C]+760") -- X
        y_output = My["my_Y"] -- = readInteger("[pcsx2-r3878.exe+0040239C]+768") -- Y
        z_output = My["my_Z"] -- = readInteger("[pcsx2-r3878.exe+0040239C]+764") -- Z
        f_output = My["my_F"] -- = readInteger("[pcsx2-r3878.exe+0040239C]+730") -- F

        file:write(x_output);
        file:write("\n");
        file:write(y_output);
        file:write("\n");
        file:write(z_output);
        file:write("\n");
        file:write(f_output);
        file:write("\n");
        file:close();
        co(_sender, "co_debugoutput", "File Closed")
        
        co(_sender, "co_debugoutput", "Output X, Y, Z & F For Writing..")
        co(_sender, "co_debugoutput", "x: " .. tostring(x_output))
        co(_sender, "co_debugoutput", "y: " .. tostring(y_output))
        co(_sender, "co_debugoutput", "z: " .. tostring(z_output))
        co(_sender, "co_debugoutput", "f: " .. tostring(f_output))
    else
        co(_sender, "co_debugoutput", "Error: File Does Not Exist: " .. _filepath)
    end

    co(_sender, "co_exit", "")
end
function InputDBData()
    local _sender = "InputDBData"
    co(_sender, "co_enter", "")
    local file_name = ""
    if Thisbox == "Box1" then
        file_name = "Box2"
    else
        file_name = "Box1"
    end
    local _filepath = Folder_DBData .. file_name .. Extension_DBData
    if File_Exists(_filepath) == true then
        co(_sender, "co_debugoutput", "File Location: " .. _filepath)
        file = io.open(_filepath, "r+");
        x_input = file:read()
        y_input = file:read()
        z_input = file:read()
        f_input = file:read()
        file:close();
        co(_sender, "co_debugoutput", "File Closed")
        co(_sender, "co_debugoutput","Output X, Y, Z & F For Reading..")
        co(_sender, "co_debugoutput", "x: " .. tostring(x_input))
        co(_sender, "co_debugoutput", "y: " .. tostring(y_input))
        co(_sender, "co_debugoutput", "z: " .. tostring(z_input))
        co(_sender, "co_debugoutput", "f: " .. tostring(f_input))
        --co(_sender, "co_debugoutput","Output Current NPC Values..")
        --co(_sender, "co_debugoutput", "NPC X: " .. tostring(NPCs[1]:XGet()))
        --co(_sender, "co_debugoutput", "NPC Y: " .. tostring(NPCs[1]:YGet()))
        --co(_sender, "co_debugoutput", "NPC Z: " .. tostring(NPCs[1]:ZGet()))
        --co(_sender, "co_debugoutput", "NPC F: " .. tostring(NPCs[1]:FGet()))
        --co(_sender, "co_debugoutput","Set NPC Values to Read Values..")
        NPCs[1].XSet(x_input)
        NPCs[1].YSet(y_input)
        NPCs[1].ZSet(z_input)
        NPCs[1].FSet(f_input)
        --co(_sender, "co_debugoutput","Output New Current NPC Values..")
        --co(_sender, "co_debugoutput", "NPC X: " .. tostring(NPCs[1]:XGet()))
        --co(_sender, "co_debugoutput", "NPC Y: " .. tostring(NPCs[1]:YGet()))
        --co(_sender, "co_debugoutput", "NPC Z: " .. tostring(NPCs[1]:ZGet()))
        --co(_sender, "co_debugoutput", "NPC F: " .. tostring(NPCs[1]:FGet()))
    else
        co(_sender, "co_debugoutput", "Error: File Does Not Exist: " .. _filepath)
    end
    
    co(_sender, "co_exit", "")
end
function oldOutputDBData()
    local _sender = "OutputDBData"
    co(_sender, "co_enter", "")
    local file_name = ""
    if Thisbox == "Box1" then
        file_name = "Box1"
    else
        file_name = "Box2"
    end
    _filepath = Folder_DBData .. file_name .. Extension_DBData
    if File_Exists(_filepath) == true then
        --co(_sender, "co_debugoutput", "File Location: " .. _filepath)
        file = io.open(_filepath,"w+");
        
        x_output = readInteger("[pcsx2-r3878.exe+0040239C]+760") -- X
        y_output = readInteger("[pcsx2-r3878.exe+0040239C]+768") -- Y
        z_output = readInteger("[pcsx2-r3878.exe+0040239C]+764") -- Z
        f_output = readInteger("[pcsx2-r3878.exe+0040239C]+730") -- F

        file:write(x_output);
        file:write("\n");
        file:write(y_output);
        file:write("\n");
        file:write(z_output);
        file:write("\n");
        file:write(f_output);
        file:write("\n");
        file:close();
        co(_sender, "co_debugoutput", "File Closed")
        
        co(_sender, "co_debugoutput", "Output X, Y, Z & F For Writing..")
        co(_sender, "co_debugoutput", "x: " .. tostring(x_output))
        co(_sender, "co_debugoutput", "y: " .. tostring(y_output))
        co(_sender, "co_debugoutput", "z: " .. tostring(z_output))
        co(_sender, "co_debugoutput", "f: " .. tostring(f_output))
    else
        co(_sender, "co_debugoutput", "Error: File Does Not Exist: " .. _filepath)
    end


    co(_sender, "co_exit", "")
end
function oldInputDBData()
    local _sender = "InputDBData"
    co(_sender, "co_enter", "")
    local file_name = ""
    if Thisbox == "Box1" then
        file_name = "Box2"
    else
        file_name = "Box1"
    end
    local _filepath = Folder_DBData .. file_name .. Extension_DBData
    if File_Exists(_filepath) == true then
        co(_sender, "co_debugoutput", "File Location: " .. _filepath)
        file = io.open(_filepath, "r+");
        x_input = file:read()
        y_input = file:read()
        z_input = file:read()
        f_input = file:read()
        file:close();
        co(_sender, "co_debugoutput", "File Closed")
        co(_sender, "co_debugoutput","Output X, Y, Z & F For Reading..")
        co(_sender, "co_debugoutput", "x: " .. tostring(x_input))
        co(_sender, "co_debugoutput", "y: " .. tostring(y_input))
        co(_sender, "co_debugoutput", "z: " .. tostring(z_input))
        co(_sender, "co_debugoutput", "f: " .. tostring(f_input))
        --co(_sender, "co_debugoutput","Output Current NPC Values..")
        --co(_sender, "co_debugoutput", "NPC X: " .. tostring(NPCs[1]:XGet()))
        --co(_sender, "co_debugoutput", "NPC Y: " .. tostring(NPCs[1]:YGet()))
        --co(_sender, "co_debugoutput", "NPC Z: " .. tostring(NPCs[1]:ZGet()))
        --co(_sender, "co_debugoutput", "NPC F: " .. tostring(NPCs[1]:FGet()))
        --co(_sender, "co_debugoutput","Set NPC Values to Read Values..")
        NPCs[1].XSet(x_input)
        NPCs[1].YSet(y_input)
        NPCs[1].ZSet(z_input)
        NPCs[1].FSet(f_input)
        --co(_sender, "co_debugoutput","Output New Current NPC Values..")
        --co(_sender, "co_debugoutput", "NPC X: " .. tostring(NPCs[1]:XGet()))
        --co(_sender, "co_debugoutput", "NPC Y: " .. tostring(NPCs[1]:YGet()))
        --co(_sender, "co_debugoutput", "NPC Z: " .. tostring(NPCs[1]:ZGet()))
        --co(_sender, "co_debugoutput", "NPC F: " .. tostring(NPCs[1]:FGet()))
    else
        co(_sender, "co_debugoutput", "Error: File Does Not Exist: " .. _filepath)
    end
    
    co(_sender, "co_exit", "")
end

--Setup Functions
function setupGhosts()
    local _sender = "setupGhosts"
    co(_sender, "co_enter", "")
    local i
    name = {};
    i = 1
    if SaveState == nil or SaveState == "Corsten" then
        if SaveState == nil then co(_sender, "co_debugoutput", "Notice: SaveState variable is nil") end
        name[1] = "Coachman_Ronks"
        name[2] = "Aloj_Tilsteran"
        name[3] = "Merchant_Kari"
        name[4] = "Dr_Killian"
        name[5] = "Guard_Saolen"
        name[6] = "Guard_Jahn"
        name[7] = "Bowyer_Koll"
        name[8] = "Tailor_Bariston"
        name[9] = "Tailor_Zixar"
        name[10] = "Guard_Serenda"
        name[11] = "a_badger"
        name[12] = "an_undead_mammoth"
        name[13] = "Angry_Patron"
        name[14] = "Arch_Familiar"
        name[15] = "Finalquestt"
        name[16] = "Guard_Perinen"
        name[17] = "Manoarmz"
        name[18] = "Marona_Jofranka"
        name[19] = "Merchant_Ahkham"
        name[20] = "Nukenurplace"
        name[21] = "Raam"
        name[22] = "Royce_Tilsteran"
        --name[23] = "sign_post"
        while name[i] ~= nil do
            Ghosts[i] = Ghost.new(name[i])
            Ghosts[i].isFree = true
            i = i + 1
        end
        NumberofGhosts = i
    elseif SaveState == "Lostologist" then    
        name[1] = "elder_spirit"
        name[2] = "evil_head"
        name[3] = "kappa_drudge1"
        name[4] = "kappa_drudge2"
        name[5] = "kappa_drudge3"
        name[6] = "kappa_drudge5"
        name[7] = "phantom1"
        name[8] = "phantom2"
        name[9] = "phantom3"
        name[10] = "phantom4"
        name[11] = "phantom5"
        name[12] = "phantom6"
        name[13] = "phantom7"
        name[14] = "phantom8"
        name[15] = "phantom9"
        name[16] = "Spectral_Servant"
        name[17] = "spirit1"
        name[18] = "spirit2"
        name[19] = "spirit3"
        name[20] = "spirit4"
        name[21] = "spirit5"
        name[22] = "spirit6"
        name[23] = "spirit7"
        while name[i] ~= nil do
            Ghosts[i] = Ghost.new(name[i])
            Ghosts[i].isFree = true
            i = i + 1
        end
        NumberofGhosts = i
    else
        co(_sender, "co_debugoutput", "Error: No Ghosts code for the SaveState: " .. SaveState)
    end
    
    co(_sender, "co_debugoutput", "Number of Ghosts Loaded = " .. NumberofGhosts)
    co(_sender, "co_exit", "")
end

elapsed_times = {};
elapsed_times_last = {};
elapsed_time = 0 -- How much time has passed since you started counting
function et(_time, _mode, _threshold)
    if _time == "SETUP" then
        elapsed_times[_mode] = _threshold
        elapsed_times_last[_mode] = 0
    elseif elapsed_time - elapsed_times_last[_time] > elapsed_times[_time] then
        elapsed_times_last[_time] = elapsed_time
        return true
    elseif elapsed_time - elapsed_times_last[_time] < elapsed_times[_time] then
        return false
    end
end

mytimer = nil
if mytimer == nil then
    if File_Exists(Folder_LUA_Modes .. mode .. "/" .. "Settings" .. ".lua") then dofile(Folder_LUA_Modes .. mode .. "/" .. "Settings" .. ".lua") end
    local _sender = "mytimer == nil"
    setupGhosts()
    co(_sender, "co_debugoutput", "Mode == \"" .. mode .. "\"")
    
    interval_timer = 100 -- How often the Timer code runs (1 = .0001seconds, 10 = .01, 100 = .1, 1000 = 1).
    mytimer=createTimer(nil) -- Creates the variable mytimer to be a code 'object' that is a Timer.
    mytimer.Interval=interval_timer -- This makes the timer's interval equal to the variable interval_timer.


    _sender = "mytimer.OnTimer=function"
    co(_sender, "co_enter", _sender)
    mytimer.OnTimer=function(t) -- GAME LOGIC
        -- Do programming for special things here.
        -- Code in this area will execute every single time this loop executes.
        
        -- Read Start and Select, if both are pressed Toggle Console Output on or off.
        value_Start = readBytes("[pcsx2-r3878.exe+0040239C]+980")
        value_Select = readBytes("[pcsx2-r3878.exe+0040239C]+981")
        if value_Select == 1 and value_Start == 1 then
            if doCOOutput == true then
                print("Shutting off Console Output...")
                doCOOutput = false
            elseif doCOOutput == false then
                print("Turning on Console Output...")
                doCOOutput = true
            end
        end

        -- The below if statement controls Modes.
        -- If the "mode" variable is the present mode,
        --   and also if so much time has passed that the code for the present mode
        --   needs ran again, run it.
        if et(mode) and File_Exists(Folder_LUA_Modes .. mode .. "/" .. mode .. ".lua") == true then
            LoopFunction()
        elseif et(mode) then -- Else if the mode file doesn't exist but it is time to run it
            co(_sender, "co_debugoutput", "Error: No Mode File Found")
        end

        elapsed_time = elapsed_time + interval_timer
    end
end
mytimer.Enabled=true

function LoopFunction()
    -- Beginning Loop Stuff
    _sender = "LoopFunction"
    co(_sender, "co_enter", "")
    if LogicLoopsStarted ~= LogicLoopsEnded then
        co(_sender, "co_debugoutput", "Error: The Last Logic Loop Did Not Complete.")
        LogicLoopsEnded = LogicLoopsEnded + 1
    end
    LogicLoopsStarted = LogicLoopsStarted + 1
    co(_sender, "co_comment", "Started Logic Loop " .. LogicLoopsStarted)
    
    -- Current Mode's PreCode Runs Now
    --dofile(Folder_LUA_Modes .. mode .. "/PreCode.lua")
    
    -- Add ordering schema to below, so you can inject your own processes where you need to.
    if doUpdateValues then updateValues() end
    if doUpdateAreas then updateAreas() end
    if doOutputLocation then outputLocation() end
    if doOutsideCommands then ProcessOutsideCommands() end
    
    -- Current Mode Runs Now
    dofile(Folder_LUA_Modes .. mode .. "/" .. mode .. ".lua")
    
    -- Ending Loop Stuff
        _sender = "LoopFunction_End"
    co(_sender, "co_enter", "")
    
    -- Clear Updated Flags
    WasUpdated["my_Location"] = false
    WasUpdated["my_X"] = false
    WasUpdated["my_Y"] = false
    WasUpdated["my_Z"] = false
    WasUpdated["my_F"] = false
    WasUpdated["my_ZoneFull"] = false
    WasUpdated["my_Zone"] = false
    WasUpdated["my_ZoneSub"] = false
    --Clear WasUpdated[] to false ---
    --for key,value in pairs(WasUpdated) do
    --    WasUpdated[key] = false
    --end
    
    
    -- Current Mode's PostCode Runs Now
    --dofile(Folder_LUA_Modes .. mode .. "/PostCode.lua")
    
    LogicLoopsEnded = LogicLoopsEnded + 1
    co(_sender, "co_comment", "Completed Logic Loop " .. LogicLoopsEnded)

    -- If co() is supposed to do file output based on logic loops, trigger that now
    if Left(output_file_lines[0], 2) == "ll" then
        _loopstodo = tonumber(Right(output_file_lines[0], string.len(output_file_lines[0]) - 2))
        if LogicLoopsEnded == _loopstodo then
            co(_sender, "do_fileoutput")
            output_file_lines[0] = 0
        elseif LogicLoopsEnded > _loopstodo then
            _loopsdifference = LogicLoopsEnded - _loopstodo
            co(_sender, "co_debugoutput", "Error: Console Outputting " .. _loopsdifference .. " Logic Loops Past Due")
            co(_sender, "co_debugoutput", "  This is caused by a code error that prevented the previous loop(s) from finishing.")
            co(_sender, "do_fileoutput")
            output_file_lines[0] = 0
        end
    end
end


--[[ To [Maybe] Do List
Millisecond Converter Function
File Reader Function
File Writer Function
Add Arguments to Force The Use of a Specified Old File Version
Add NPCs to where walls are when creating them.
-Add these NPCs to your group so you can target them easily.

]]--

            --[[ -- Other Write String to Bit Array Function Versions
function currentWrite_StringToBitArrayAddress(_string, BitArrayAddress)
    --_sender = "Write_StringToBitArrayAddress"
    --co(_sender, "co_enter", "String, BitArrayAddress")
    local sp
    local i
    local i2
    sp = 1
    i = 1
    --i2 = i + 1
    StringAsArrayofBits = {};
    while sp <= string.len(_string) do
        StringAsArrayofBits[i] = string.byte(_string, sp)
        --StringAsArrayofBits[i2] = 00
        co(_sender, "co_debugoutput", StringAsArrayofBits[i])
        sp = sp + 1
        i = i + 1
        --i = i + 2
        --i2 = i + 1
    end
    StringAsArrayofBits[i] = 00
    StringAsArrayofBits[i + 1] = 00
    --StringAsArrayofBits[i2] = 00
    writeBytes(BitArrayAddress, StringAsArrayofBits)
    
    if Read_BitArrayToString(BitArrayAddress, "TypeZoneName") == _string then
        co(_sender, "co_debugoutput", _sender .. " thinks it worked. Did it?..")
    else
        co(_sender, "co_debugoutput", _sender .. " has detected that it didn't work. Did it?..")
    end    
end
function newWrite_StringToBitArrayAddress(String, BitArrayAddress)
    _sender = "Write_StringToBitArrayAddress"
    co(_sender, "co_enter", "String, BitArrayAddress")
    local sp
    local i
    local i2
    sp = 1
    i = 1
    i2 = i + 1
    StringAsArrayofBits = {};
    while sp <= string.len(String) do
        StringAsArrayofBits[i] = string.byte(String, sp)
        StringAsArrayofBits[i2] = 00
        co(_sender, "co_debugoutput", StringAsArrayofBits[i])
        sp = sp + 1
        i = i + 2
        i2 = i + 1
    end
    StringAsArrayofBits[i] = 00
    StringAsArrayofBits[i2] = 00
    writeBytes(BitArrayAddress, StringAsArrayofBits)
    
        
    if Read_BitArrayToString(BitArrayAddress, "TypeZoneName") == _string then
        co(_sender, "co_debugoutput", _sender .. " thinks it worked. Did it?..")
    else
        co(_sender, "co_debugoutput", _sender .. " has detected that it didn't work. Did it?..")
    end
    
    end
function oldWrite_StringToBitArrayAddress(String, BitArrayAddress)
    --_sender = "Write_StringToBitArrayAddress"
    --co(_sender, "co_enter", "String, BitArrayAddress")
    local sp
    local i
    local i2
    sp = 1
    i = 1
    --i2 = i + 1
    StringAsArrayofBits = {};
    while sp <= string.len(String) do
        StringAsArrayofBits[i] = string.byte(String, sp)
        --StringAsArrayofBits[i2] = 00
        --co(_sender, "co_debugoutput", StringAsArrayofBits[i])
        sp = sp + 1
        i = i + 1
        --i = i + 2
        --i2 = i + 1
    end
    StringAsArrayofBits[i] = 00
    StringAsArrayofBits[i + 1] = 00
    --StringAsArrayofBits[i2] = 00
    writeBytes(BitArrayAddress, StringAsArrayofBits)
end]]--
            --[[-- Zone Manipulation Here
            -- Zone Manipulation Here
            --
            local ZoneAsString
            --ZoneAsString = Read_BitArrayToString("[pcsx2-r3878.exe+00400B28]+D30", 32, true)
            ZoneAsString = Read_BitArrayToString("[pcsx2-r3878.exe+00400B28]+D30", "TypeZoneName")
            co(_sender, "co_debugoutput", "Zone: " .. ZoneAsString .. ":")
            
            if Left(ZoneAsString, 4) ~= "New " then
                NewZoneName = "New " .. ZoneAsString
                co(_sender, "co_debugoutput", "Changing Zone to " .. NewZoneName)
                Write_StringToBitArrayAddress(NewZoneName, "[pcsx2-r3878.exe+00400B28]+D30", 2)
            end
            --
            -- 
            --]]--
