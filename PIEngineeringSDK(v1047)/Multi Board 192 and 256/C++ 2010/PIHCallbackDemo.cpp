// PIHCallbackDemo.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "resource.h"
#include <stdlib.h>
#include "PIEHid32.h"

#include <string.h>
#include <stdio.h>

#define MAXDEVICES  512   //max allowed array size for enumeratepie =128 devices*4 bytes per device


// function declares 
int CALLBACK DialogProc(
  HWND hwndDlg,  // handle to dialog box
  UINT uMsg,     // message
  WPARAM wParam, // first message parameter
  LPARAM lParam  // second message parameter
);
void FindAndStart(HWND hDialog);
void AddEventMsg(HWND hDialog, char *msg);
DWORD __stdcall HandleDataEvent(UCHAR *pData, DWORD deviceID, DWORD error);
DWORD __stdcall HandleErrorEvent(DWORD deviceID, DWORD status);

BYTE buffer[80];  //used for writing to device

HWND hDialog;
long hDevice = -1;

int saveled;
bool keyboard=false; //for keyboard reflector sample
bool mouseon=false; //for mouse reflector sample
bool lastprogsw=false;

//---------------------------------------------------------------------

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{
    DWORD result;
	MSG   msg;

	hDialog = CreateDialog(hInstance, (LPCTSTR)IDD_MAIN, NULL, DialogProc);

	ShowWindow(hDialog, SW_NORMAL);

	//put numbers in edit boxes
	HWND hList;
	hList = GetDlgItem(hDialog, IDC_JoyX);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyY);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyZ);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyZr);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoySlider);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyGame1);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyGame2);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyGame3);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyGame4);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_JoyHat);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"8");

	hList = GetDlgItem(hDialog, IDC_MouseButtons);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");
	hList = GetDlgItem(hDialog, IDC_MouseX);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"15");
	hList = GetDlgItem(hDialog, IDC_MouseY);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"15");
	hList = GetDlgItem(hDialog, IDC_MouseWheel);
	if (hList == NULL) return 0;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)(LPCTSTR)"0");

	result = GetMessage( &msg, NULL, 0, 0 );
	while (result != 0)    { 
		if (result == -1)    {
			return 1;
			// handle the error and possibly exit
		}
		else    {
			TranslateMessage(&msg); 
			DispatchMessage(&msg); 
		}
		
		result = GetMessage( &msg, NULL, 0, 0 );
	}

    if (hDevice != -1) CleanupInterface(hDevice);

	return 0;
}
//---------------------------------------------------------------------

int CALLBACK DialogProc(
  HWND hwndDlg,  // handle to dialog box
  UINT uMsg,     // message
  WPARAM wParam, // first message parameter
  LPARAM lParam  // second message parameter
)    {

	DWORD result;
//	BYTE buffer[80];  //this globally declared only to keep track of LEDs	
	
	HWND hList;
	

	int countout=0;
	int wlen=GetWriteLength(hDevice);
	char errordescription[100]; //used with the GetErrorString call to describe the error

	
	switch (uMsg)    {
	case WM_INITDIALOG:
		SendMessage(GetDlgItem(hwndDlg, CHK_NONE), BM_SETCHECK, BST_CHECKED, 0);
		// Indicate that events are off to start.
		return FALSE;   // not choosing keyboard focus

    case WM_COMMAND:
		switch (wParam)    {
		case IDCANCEL:
			PostQuitMessage(0);
			return TRUE;

		case IDSTART:
		    if (hDevice != -1) CloseInterface(hDevice);
			// only one at a time in the demo, please

			hDevice = -1;
			//SendMessage(GetDlgItem(hwndDlg, CHK_NONE), BM_CLICK, 0, 0);
			FindAndStart(hwndDlg);
			return TRUE;

		case IDHALT:
		    if (hDevice != -1) CloseInterface(hDevice);
			hDevice = -1;
			return TRUE;

       case IDC_CALLBACK:
			if (hDevice == -1) return TRUE;
			//Turn on the data callback
			result = SetDataCallback(hDevice, HandleDataEvent);
			result = SetErrorCallback(hDevice, HandleErrorEvent);
			if (result != 0)    {
				AddEventMsg(hwndDlg, "Error:");
				GetErrorString(result, errordescription, 100);
				AddEventMsg(hwndDlg, errordescription);
			}
			SuppressDuplicateReports(hDevice, true);
            DisableDataCallback(hDevice, false); //turn on callback in the case it was turned off by some other command
			return TRUE;

		case IDC_CLEAR:
		    
			hList = GetDlgItem(hDialog, ID_EVENTS);
			if (hList == NULL) return TRUE;
			SendMessage(hList, LB_RESETCONTENT, 0, 0);
			return TRUE;

		case IDC_UNITID:
			//this writes the unit ID entered in IDC_EDIT1. 0-255 possible.
			memset(buffer, 0, 80);
			for (int i=0;i<36;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=189;
			
			//get text box text
			hList = GetDlgItem(hDialog, IDC_EDIT1);
			if (hList == NULL) return TRUE;
			char UnitID[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)UnitID);
			buffer[2]= atoi(UnitID);
			
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED1:
			//only works if disable keyboard control
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 1;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED2:
			//only works if disable keyboard control
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 2;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED3:
			//only works if disable keyboard control
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 4;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

	    case IDC_LED4:
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 8;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED5:
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 16;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED6:
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 32;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED7:
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 64;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_LED8:
			saveled=buffer[2];
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			buffer[2]=saveled | 128;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_NOLED:
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=186;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_CHKKeyboard:
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=184; 
			//get checked state
			hList = GetDlgItem(hDialog, IDC_CHKKeyboard);
			if (hList == NULL) return TRUE;
			if (SendMessage(hList, BM_GETCHECK, 0, 0)!=BST_CHECKED) buffer[2]=1; //0=disable, 1=enable
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_GENERATE:
			 //After sending this command a general incoming data report will be given with
            //the 3rd byte (Data Type) 2nd bit set.  If program switch is up byte 3 will be 2
            //and if it is pressed byte 3 will be 3.  This is useful for getting the initial state
            //or unit id of the device before it sends any data.
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=177;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_TOREFLECT:
			//Change device to reflector mode, the default mode as shipped
            //In reflector mode the device has 2 additional endpoints; keyboard and mouse/joystick
            for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			//0:Reflector mode.  Reporting to the USB as a Splat device (IN and OUT), a joystick, and a keyboard.
            //1: Hardware mode*. Reporting to the USB as a Splat (OUT only) device a joystick, and a keyboard.
            //2: Traditional Splat mode. Reporting as only a Splat  device (IN and OUT).
			//*Hardware mode not addressed in this sdk  
			buffer[0]=0;
			buffer[1]=204;
			buffer[2]=0; //mode
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_TOSPLAT:
			//Change device to reflector mode, the default mode as shipped
            //In reflector mode the device has 2 additional endpoints; keyboard and mouse/joystick
            for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			//0:Reflector mode.  Reporting to the USB as a Splat device (IN and OUT), a joystick, and a keyboard.
            //1: Hardware mode*. Reporting to the USB as a Splat (OUT only) device a joystick, and a keyboard.
            //2: Traditional Splat mode. Reporting as only a Splat  device (IN and OUT).
			//*Hardware mode not addressed in this sdk 
			buffer[0]=0;
			buffer[1]=204;
			buffer[2]=2; //mode
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_TIMESTAMP:
			//Sending this command will turn off the 4 bytes of data which assembled give the time in ms from the start of the computer
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=210;
			buffer[2]=0; //0 to turn off time stamp, 1 to turn on time stamp
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_TIMESTAMP2:
			//Sending this command will turn on the 4 bytes of data which assembled give the time in ms from the start of the computer
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=210;
			buffer[2]=1; //0 to turn off time stamp, 1 to turn on time stamp
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_KEYREFLECT:
			//Sends keyboard commands as a native keyboard to textbox
			hList = GetDlgItem(hDialog, IDC_EDIT2);
			if (hList == NULL) return TRUE;
			SetFocus(hList);
			for(int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=201;
			buffer[2]=2; //modifiers Bit 1=Left Ctrl, bit 2=Left Shift, bit 3=Left Alt, bit 4=Left Gui, bit 5=Right Ctrl, bit 6=Right Shift, bit 7=Right Alt, bit 8=Right Gui.
			buffer[3]=0; //always 0
			buffer[4]=0x04; //1st hid code a, down
			buffer[5]=0; //2nd hid code
			buffer[6]=0; //3rd hid code
			buffer[7]=0; //4th hid code
			buffer[8]=0; //5th hid code
			buffer[9]=0; //6th hid code
			result=404;
			while(result==404)
			{
				result=WriteData(hDevice, buffer);
			}
			//Sleep(100);
			buffer[0]=0;
			buffer[1]=201;
			buffer[2]=0; //modifiers Bit 1=Left Ctrl, bit 2=Left Shift, bit 3=Left Alt, bit 4=Left Gui, bit 5=Right Ctrl, bit 6=Right Shift, bit 7=Right Alt, bit 8=Right Gui.
			buffer[3]=0; //always 0
			buffer[4]=0; //1st hid code a up
			buffer[5]=0x05; //2nd hid code b down
			buffer[6]=0x06; //3rd hid code c down
			buffer[7]=0x07; //4th hid code d down
			buffer[8]=0; //5th hid code
			buffer[9]=0; //6th hid code
			result=404;
			while(result==404)
			{
				result=WriteData(hDevice, buffer);
			}
			//Sleep(100);
			buffer[0]=0;
			buffer[1]=201;
			buffer[2]=0; //modifiers Bit 1=Left Ctrl, bit 2=Left Shift, bit 3=Left Alt, bit 4=Left Gui, bit 5=Right Ctrl, bit 6=Right Shift, bit 7=Right Alt, bit 8=Right Gui.
			buffer[3]=0; //always 0
			buffer[4]=0; //1st hid code 
			buffer[5]=0; //2nd hid code b up
			buffer[6]=0; //3rd hid code c up
			buffer[7]=0; //4th hid code d up
			buffer[8]=0; //5th hid code
			buffer[9]=0; //6th hid code
			result=404;
			while(result==404)
			{
				result=WriteData(hDevice, buffer);
			}
			//Sleep(100);
			return TRUE;
		case IDC_JOYREFLECT:
			//open up the game controller control panel to test these features, after clicking this button
            //go and make active the control panel properties and change will occur
			//only available in PID #1 and PID #3
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=202;
			
			char joyval[10];
			hList = GetDlgItem(hDialog, IDC_JoyX);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[2]=atoi(joyval)^127-255; //X, 0 to 127 from center to right, 255 to 128 from center to left
			hList = GetDlgItem(hDialog, IDC_JoyY);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[3]=atoi(joyval)^127; //Y, 0 to 127 from center down, 255 to 128 from center up
			hList = GetDlgItem(hDialog, IDC_JoyZr);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[4]=atoi(joyval)^127; //Z rotation, 0 to 127 from center down, 255 to 128 from center up
			hList = GetDlgItem(hDialog, IDC_JoyZ);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[5]=atoi(joyval)^127; //Z, 0 to 127 from center down, 255 to 128 from center up
			hList = GetDlgItem(hDialog, IDC_JoySlider);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[6]=atoi(joyval)^127; //Slider, 0 to 127 from center down, 255 to 128 from center up
			
			hList = GetDlgItem(hDialog, IDC_JoyGame1);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[7]=atoi(joyval); //buttons 1-8, where bit 1 is button 1, bit 2 is button 2
			hList = GetDlgItem(hDialog, IDC_JoyGame2);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[8]=atoi(joyval); //buttons 9-16, where bit 1 is button 1, bit 2 is button 2
			hList = GetDlgItem(hDialog, IDC_JoyGame3);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[9]=atoi(joyval); //buttons 17-24, where bit 1 is button 1, bit 2 is button 2
			hList = GetDlgItem(hDialog, IDC_JoyGame4);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[10]=atoi(joyval); //buttons 25-32, where bit 1 is button 1, bit 2 is button 2
			
			buffer[11]=0;

			hList = GetDlgItem(hDialog, IDC_JoyHat);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)joyval);
			buffer[12]=atoi(joyval); //hat, where 0 is straight up, 1 is 45deg cw, etc and 8 is no hat
			
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;
		case IDC_MOUSEREFLECT3:
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=203;
			
			char val[10];
			hList = GetDlgItem(hDialog, IDC_MouseButtons);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)val);
			buffer[2]=atoi(val); //1=left, 2=right, 4=center, 8=XButton1, 16=XButton2
			hList = GetDlgItem(hDialog, IDC_MouseX);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)val);
			buffer[3]=atoi(val); //X motion, 128=0 no motion, 1-127 is right, 255-129=left, finest inc (1 and 255) to coarsest (127 and 129)
			hList = GetDlgItem(hDialog, IDC_MouseY);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)val);
			buffer[4]=atoi(val); //Y motion, 128=0 no motion, 1-127 is down, 255-129=up, finest inc (1 and 255) to coarsest (127 and 129)
			buffer[5]=0; //Wheel X
			hList = GetDlgItem(hDialog, IDC_MouseWheel);
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)val);
			buffer[6]=atoi(val); //Wheel Y, 128=0 no motion, 1-127 is up, 255-129=down, finest inc (1 and 255) to coarsest (127 and 129).
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;
		case IDC_DESCRIPTOR:
			//turn off callback
			DisableDataCallback(hDevice, true); //turn off callback so capture data here
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=214;
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			//after this write the next read 3rd byte=214 will give descriptor information
			for (int i=0;i<80;i++)
			{buffer[i]=0;}
			
			
			result = BlockingReadData(hDevice, buffer, 100);
			
			while (result == 304 || (result == 0 && buffer[2] != 214))
			{
				if (result == 304)
				{
					// No data received after 100ms, so increment countout extra
					countout += 99;
				}
				countout++;
				if (countout > 1000) //increase this if have to check more than once
					break;
				result = BlockingReadData(hDevice, buffer, 100);
			}
			
			if (result ==0 && buffer[2]==214)
			{
				char dataStr[256];
				//clear out listbox
				hList = GetDlgItem(hDialog, ID_EVENTS);
				if (hList == NULL) return TRUE;
				SendMessage(hList, LB_RESETCONTENT, 0, 0);

				if (buffer[3]==0) AddEventMsg(hDialog, "PID #1");
				else if (buffer[3]==2) AddEventMsg(hDialog, "PID #2");
				
				_itoa_s(buffer[4],dataStr,10);
				char str[80];
				strcpy_s (str,"Keymapstart ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);

				_itoa_s(buffer[5],dataStr,10);
				strcpy_s (str,"Layer2offset ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);

				_itoa_s(buffer[6],dataStr,10);
				strcpy_s (str,"OutSize ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);

				_itoa_s(buffer[7],dataStr,10);
				strcpy_s (str,"ReportSize ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);
				
				_itoa_s(buffer[8],dataStr,10);
				strcpy_s (str,"MaxCol ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);

				_itoa_s(buffer[9],dataStr,10);
				strcpy_s (str,"MaxRow ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);

				strcpy_s (str,"");
				if (buffer[10]&64) strcpy_s (str,"Green LED ");
				if (buffer[10]&128) strcat_s (str,"Red LED ");
				if (strlen(str)==0) strcpy_s (str,"None ");
				AddEventMsg(hDialog, str);

				_itoa_s(buffer[11],dataStr,10);
				strcpy_s (str,"Version ");
				strcat_s (str,dataStr);
				AddEventMsg(hDialog, str);
			
				_itoa_s((buffer[13]*256+buffer[12]),dataStr,10);
				strcpy_s (str, "PID=");
				strcat_s(str, dataStr);
				AddEventMsg(hDialog, str);
			}
			return TRUE;

		case IDC_NoDiodes:
			//send this command if no external diodes are present in the key matrix, only need to do once and setting is written to eeprom 
			//factory default
			if (hDevice == -1) return TRUE;
           
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=215; //set diodes command
			buffer[2]=1; //1 for no diodes (factory default), 0 for diodes
			
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

		case IDC_Diodes:
			//send this command if external diodes are in the key matrix
			if (hDevice == -1) return TRUE;
           
			for (int i=0;i<wlen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=215; //set diodes command
			buffer[2]=0; //1 for no diodes (factory default), 0 for diodes
			
			result=404;
			while (result==404)
			{
				result = WriteData(hDevice, buffer);
			}
			return TRUE;

	
		
		}

		break;
	}

	return FALSE;
}
//---------------------------------------------------------------------

void FindAndStart(HWND hDialog)
{
	DWORD result;
	TEnumHIDInfo info[128]; 
	long  hnd;
	long  count;
	int pid;

	result = EnumeratePIE(0x5F3, info, count);

	if (result != 0)    
	{
		if (result==102){
			AddEventMsg(hDialog, "No PI Engineering Devices Found");
		}
		else{
		AddEventMsg(hDialog, "Error finding PI Engineering Devices");
		}
		return;
	} 
	else if (count == 0)    {
		AddEventMsg(hDialog, "No PI Engineering Devices Found");
		return;
	}

	for (long i=0; i<count; i++)    {
		pid=info[i].PID; //get the device pid
		int hidusagepage=info[i].UP; //hid usage page
		int version=info[i].Version;
		int writelen=GetWriteLength(info[i].Handle);

		if ((hidusagepage == 0xC && writelen==36))    
		{
			hnd = info[i].Handle; //handle required for piehid32.dll calls
			result = SetupInterfaceEx(hnd);
			if (result != 0)    {
				AddEventMsg(hDialog, "Error setting up PI Engineering Device");
			}
			else    {
				hDevice = hnd;
				if (pid==1040)
				{
					AddEventMsg(hDialog, "Found Device: X-keys Multi Board 192, PID=1040 or PID #1");
				}
				else if (pid==1041)
				{
					AddEventMsg(hDialog, "Found Device: X-keys Multi Board 192, PID=1041");
				}
				else if (pid==1042)
				{
					AddEventMsg(hDialog, "Found Device: X-keys Multi Board 192, PID=1042 or PID #2");
				}
				else if (pid==1043)
				{
					AddEventMsg(hDialog, "Found Device: X-keys Multi Board 256, PID=1043 or PID #1");
				}
				else if (pid==1044)
				{
					AddEventMsg(hDialog, "Found Device: X-keys Multi Board 256, PID=1044");
				}
				else if (pid==1045)
				{
					AddEventMsg(hDialog, "Found Device: X-keys Multi Board 256, PID=1045 or PID #2");
				}
				else
				{
					AddEventMsg(hDialog, "Unknown device found");
				}
			}
		}
	}

	
}
//------------------------------------------------------------------------

void AddEventMsg(HWND hDialog, char *msg)
{
	HWND hList;
 
	int cnt=-1;
 
	hList = GetDlgItem(hDialog, ID_EVENTS);
	if (hList == NULL) return;
    cnt=SendMessage(hList, LB_GETCOUNT, 0, 0);
	SendMessage(hList, LB_ADDSTRING, 0, (LPARAM)msg);
   
	SendMessage(hList, LB_SETCURSEL, cnt, 0);
}
//------------------------------------------------------------------------

DWORD __stdcall HandleDataEvent(UCHAR *pData, DWORD deviceID, DWORD error)
{

		char dataStr[256];
		sprintf_s(dataStr, "%02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x\n", 
			pData[0], pData[1], pData[2], pData[3], pData[4], pData[5], pData[6], pData[7], pData[8], pData[9], pData[10], pData[11], pData[12], pData[13], pData[14], 
			pData[15], pData[16], pData[17], pData[18], pData[19], pData[20], pData[21], pData[22], pData[23], pData[24], pData[25], pData[26], pData[27], pData[28], pData[29], pData[30], pData[31], pData[32], pData[33], pData[34], pData[35], pData[36], pData[37], pData[38]);

		AddEventMsg(hDialog, dataStr);


	//error handling
	if (error==307)
	{
		CleanupInterface(hDevice);
		MessageBeep(MB_ICONHAND);
		AddEventMsg(hDialog, "Device Disconnected");
	}
	
	return TRUE;
}
//------------------------------------------------------------------------

DWORD __stdcall HandleErrorEvent(DWORD deviceID, DWORD status)
{
	MessageBeep(MB_ICONHAND);
	AddEventMsg(hDialog, "Error from error callback");
	return TRUE;
}
//------------------------------------------------------------------------
