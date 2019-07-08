// PIHCallbackDemo.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "resource.h"
#include <stdlib.h>
#include "PIEHid32.h"
#include "Windows.h"

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

BYTE buffer[80];  

HWND hDialog;
long hDevice = -1;
bool keyboard=false; //for keyboard reflect sample
HWND Wheel_Combo;
HWND Lever1_Combo;
HWND Lever2_Combo;
HWND Sw1_Combo;
HWND Sw2_Combo;
int wheel=1;
bool triggerit=false;
BYTE pdata[80];  //previously read data
BYTE savelastsent;

bool firstread=true;
float runningtotal;
float lasttheta;

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
	//fill in the textboxes
	HWND hList;
	hList = GetDlgItem(hDialog, IDC_EDIT1);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"110");
	hList = GetDlgItem(hDialog, IDC_EDIT2);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"121");
	hList = GetDlgItem(hDialog, IDC_EDIT3);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"118");

	hList = GetDlgItem(hDialog, IDC_EDIT4);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");

	hList = GetDlgItem(hDialog, IDC_EDIT5);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"9");

	hList = GetDlgItem(hDialog, IDC_EDIT19);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"50");
	
	hList = GetDlgItem(hDialog, IDC_EDIT6);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");
	hList = GetDlgItem(hDialog, IDC_EDIT7);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");
	hList = GetDlgItem(hDialog, IDC_EDIT8);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");
	hList = GetDlgItem(hDialog, IDC_EDIT9);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");
	hList = GetDlgItem(hDialog, IDC_EDIT10);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");
	hList = GetDlgItem(hDialog, IDC_EDIT11);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");
    hList = GetDlgItem(hDialog, IDC_EDIT13);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");

	hList = GetDlgItem(hDialog, IDC_EDIT14);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");

	hList = GetDlgItem(hDialog, IDC_EDIT15);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"0");

	hList = GetDlgItem(hDialog, IDC_EDIT12);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"8");

	hList = GetDlgItem(hDialog, IDC_SENS);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"2");

	hList = GetDlgItem(hDialog, IDC_EDIT20);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"5000");

	hList = GetDlgItem(hDialog, IDC_EDIT21);
	if (hList == NULL) return TRUE;
	SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"5000");

	//create and fill in the analog control combo boxes

	Wheel_Combo = CreateWindowEx(0, "ComboBox", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWNLIST, 500, 305, 100, 150, hDialog, NULL, hInstance, NULL); 
	SendMessage(Wheel_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick X");
	SendMessage(Wheel_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick Y");
	SendMessage(Wheel_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Rotation");
	SendMessage(Wheel_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Axis");
	SendMessage(Wheel_Combo, CB_ADDSTRING, 0, (LPARAM)"Slider");
	SendMessage(Wheel_Combo, CB_ADDSTRING, 0, (LPARAM)"None");

	Lever1_Combo = CreateWindowEx(0, "ComboBox", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWNLIST, 500, 335, 100, 150, hDialog, NULL, hInstance, NULL); 
	SendMessage(Lever1_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick X");
	SendMessage(Lever1_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick Y");
	SendMessage(Lever1_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Rotation");
	SendMessage(Lever1_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Axis");
	SendMessage(Lever1_Combo, CB_ADDSTRING, 0, (LPARAM)"Slider");
	SendMessage(Lever1_Combo, CB_ADDSTRING, 0, (LPARAM)"None");

	Lever2_Combo = CreateWindowEx(0, "ComboBox", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWNLIST, 500, 365, 100, 150, hDialog, NULL, hInstance, NULL); 
	SendMessage(Lever2_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick X");
	SendMessage(Lever2_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick Y");
	SendMessage(Lever2_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Rotation");
	SendMessage(Lever2_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Axis");
	SendMessage(Lever2_Combo, CB_ADDSTRING, 0, (LPARAM)"Slider");
	SendMessage(Lever2_Combo, CB_ADDSTRING, 0, (LPARAM)"None");

	Sw1_Combo = CreateWindowEx(0, "ComboBox", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWNLIST, 500, 395, 100, 150, hDialog, NULL, hInstance, NULL); 
	SendMessage(Sw1_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick X");
	SendMessage(Sw1_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick Y");
	SendMessage(Sw1_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Rotation");
	SendMessage(Sw1_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Axis");
	SendMessage(Sw1_Combo, CB_ADDSTRING, 0, (LPARAM)"Slider");
	SendMessage(Sw1_Combo, CB_ADDSTRING, 0, (LPARAM)"None");

	Sw2_Combo = CreateWindowEx(0, "ComboBox", NULL, WS_VISIBLE | WS_CHILD | CBS_DROPDOWNLIST, 500, 425, 100, 150, hDialog, NULL, hInstance, NULL); 
	SendMessage(Sw2_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick X");
	SendMessage(Sw2_Combo, CB_ADDSTRING, 0, (LPARAM)"Joystick Y");
	SendMessage(Sw2_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Rotation");
	SendMessage(Sw2_Combo, CB_ADDSTRING, 0, (LPARAM)"Z Axis");
	SendMessage(Sw2_Combo, CB_ADDSTRING, 0, (LPARAM)"Slider");
	SendMessage(Sw2_Combo, CB_ADDSTRING, 0, (LPARAM)"None");

	//set default
	SendMessage(Wheel_Combo, CB_SETCURSEL, 4, 0);
	SendMessage(Lever1_Combo, CB_SETCURSEL, 0, 0);
	SendMessage(Lever2_Combo, CB_SETCURSEL, 1, 0);
	SendMessage(Sw1_Combo, CB_SETCURSEL, 3, 0);
	SendMessage(Sw2_Combo, CB_SETCURSEL, 2, 0);

	result = GetMessage( &msg, NULL, TRUE, 0 );
	
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
//------------------------------------------------------------------------
void CALLBACK DlgTimerProc(HWND hwnd, UINT a, UINT b, DWORD c)
{
	//MessageBeep(MB_ICONHAND);	
	//use this for controlling completly the analogs using Joystick Reflector command.
	DisableDataCallback(hDevice, true); //turn off callback so capture data here
		
	
	BYTE rldata[80];
			
	ReadLast(hDevice, rldata);
	//check the analog bytes, if no changes then don't send anything
    bool different = false;
	for (int i = 18; i < 22; i++)
    {
        if (pdata[i] != rldata[i])
        {
            different = true;
            break;
        }
    }
    if (pdata[11] != rldata[11] || pdata[12] != rldata[12]) different = true;
    if (triggerit == true)
    {
        different = true;
        triggerit = false;
    }
	if (different==true)
	{
		for (int i=0;i<80;i++)
		{
			buffer[i]=0;
		}
		buffer[1]=202;  //joystick reflector
		//fill in the following bytes
        //wData[2] = X
        //wData[3] = Y
        //wData[4] = Z Rotation
        //wData[5] = Z Axis 
        //wData[6] = Slider

        //in the code below each combo box has these items in this order
        //Joystick X
        //Joystick Y
        //Z Rotation
        //Z Axis
        //Slider
        //(None)
		
		//Wheel
		if (SendMessage(Wheel_Combo, CB_GETCURSEL, 0, 0)!=5)
		{
            if (SendMessage(GetDlgItem(hDialog, IDC_CHKNOWRAP), BM_GETCHECK, 0, 0)!=BST_CHECKED)
            {
                WORD wheeli=rldata[11]*256+rldata[12];
				wheeli=(wheeli>>6)&0xff;
			    if (SendMessage(GetDlgItem(hDialog, IDC_CHKWHEEL), BM_GETCHECK, 0, 0)==BST_CHECKED) wheeli = 255 - wheeli; //flip polarity
				buffer[SendMessage(Wheel_Combo, CB_GETCURSEL, 0, 0) + 2]=wheeli;
			}	
			else
			{
				//use only the msb of the wheel for this
				if (pdata[11]!=rldata[11]) //don't change if no change in the msb
                {
                    
                    signed char sdelta=(rldata[11]-savelastsent);
					
					if (SendMessage(GetDlgItem(hDialog, IDC_CHKWHEEL), BM_GETCHECK, 0, 0)==BST_CHECKED) sdelta=-1*sdelta;
					if (sdelta>5)
					{
						char sens[10];
						SendMessage(GetDlgItem(hDialog, IDC_SENS), WM_GETTEXT, 8, (LPARAM)sens);
						wheel = wheel + atoi(sens);
                        if (wheel > 255) wheel = 255; //stay put
                        buffer[SendMessage(Wheel_Combo, CB_GETCURSEL, 0, 0) + 2] = (wheel ^ 0x80);
						savelastsent=rldata[11];
					}
					else if (sdelta<-5)
					{
						char sens[10];
						SendMessage(GetDlgItem(hDialog, IDC_SENS), WM_GETTEXT, 8, (LPARAM)sens);
						wheel = wheel - atoi(sens);
                        if (wheel < 0) wheel = 0; //stay put
                        buffer[SendMessage(Wheel_Combo, CB_GETCURSEL, 0, 0) + 2] = (wheel ^ 0x80);
						savelastsent=rldata[11];
					}
					else
					{
						buffer[SendMessage(Wheel_Combo, CB_GETCURSEL, 0, 0) + 2] = (wheel ^ 0x80);
					}
                }
                else
                {
                    
                   buffer[SendMessage(Wheel_Combo, CB_GETCURSEL, 0, 0) + 2] = (wheel ^ 0x80);
                }
            }
            
		} 

		//Lever 1
        if (SendMessage(Lever1_Combo, CB_GETCURSEL, 0, 0) != 5)
        {
            int lever1 = rldata[18];
            if (SendMessage(GetDlgItem(hDialog, IDC_CHKLEVER1), BM_GETCHECK, 0, 0)==BST_CHECKED) lever1 = (255 - lever1); //flip polarity
            buffer[SendMessage(Lever1_Combo, CB_GETCURSEL, 0, 0) + 2] = (lever1 ^ 0x80);    
        }

		//Lever 2
        if (SendMessage(Lever2_Combo, CB_GETCURSEL, 0, 0) != 5)
        {
            int lever2 = rldata[19];
            if (SendMessage(GetDlgItem(hDialog, IDC_CHKLEVER2), BM_GETCHECK, 0, 0)==BST_CHECKED) lever2 = (255 - lever2); //flip polarity
            buffer[SendMessage(Lever2_Combo, CB_GETCURSEL, 0, 0) + 2] = (lever2 ^ 0x80);    
        }

		//Switch 1
        if (SendMessage(Sw1_Combo, CB_GETCURSEL, 0, 0) != 5)
        {
            int sw1 = rldata[20];
            if (SendMessage(GetDlgItem(hDialog, IDC_CHKSW1), BM_GETCHECK, 0, 0)==BST_CHECKED) sw1 = (255 - sw1); //flip polarity
            buffer[SendMessage(Sw1_Combo, CB_GETCURSEL, 0, 0) + 2] = (sw1 ^ 0x80);    
        }

		//Switch 2
        if (SendMessage(Sw2_Combo, CB_GETCURSEL, 0, 0) != 5)
        {
            int sw2 = rldata[21];
            if (SendMessage(GetDlgItem(hDialog, IDC_CHKSW2), BM_GETCHECK, 0, 0)==BST_CHECKED) sw2 = (255 - sw2); //flip polarity
            buffer[SendMessage(Sw2_Combo, CB_GETCURSEL, 0, 0) + 2] = (sw2 ^ 0x80);    
        }

		//wData[2] = xaxis 0-127=center to full right, 255-128=center to full left
        //wData[3] = yaxis -127=center to bottom, 255-128=center to top
        //wData[4] = zrot 0-127=center to bottom, 255-128=center to top
        //wData[5] = zaxis 0-127=center to bottom, 255-128=center to top
        //wData[6] = slider 0-127=center to bottom, 255-128=center to top
        //wData[7] = buttons 1-8
        //wData[8] = buttons 9-16
        //wData[9] = buttons 17-24
        //wData[10] = buttons 25-32
        //wData[12] = hat
		WriteData(hDevice, buffer);

	} //end of if different = true
	for (int i=0;i<80;i++)
	{
		pdata[i] = rldata[i]; //save this data for comparison next time
	}
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
	int i=0;
	HWND hList;
	int writelen=GetWriteLength(hDevice);
	int countout=0;
	char errordescription[100]; //used with the GetErrorString call to describe the error


	switch (uMsg)    {
	case WM_INITDIALOG:
		SendMessage(GetDlgItem(hwndDlg, CHK_NONE), BM_SETCHECK, BST_CHECKED, 0);
		// Indicate that events are off to start.
		return FALSE;   // not choosing keyboard focus

    case WM_COMMAND:
		switch (wParam)    {
		case IDCANCEL:
			KillTimer(hwndDlg,0);
			PostQuitMessage(0);
			return TRUE;

		case IDSTART:
		    if (hDevice != -1) CloseInterface(hDevice);
			// only one at a time in the demo, please

			hDevice = -1;
			SendMessage(GetDlgItem(hwndDlg, CHK_NONE), BM_CLICK, 0, 0);
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
		
		case IDC_WriteDisplay:
			//this writes to the LED segments
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			//first write this to stop auto writing to display
			buffer[1]=195;
			result = WriteData(hDevice, buffer);
			
			//now write desired text
			buffer[1]=187;
			
			//get text from textboxes
			hList = GetDlgItem(hDialog, IDC_EDIT1);
			if (hList == NULL) return TRUE;
			char segment1[256];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)segment1);
			buffer[2]= atoi(segment1);
			hList = GetDlgItem(hDialog, IDC_EDIT2);
			if (hList == NULL) return TRUE;
			char segment2[256];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)segment2);
			buffer[3]= atoi(segment2);
			hList = GetDlgItem(hDialog, IDC_EDIT3);
			if (hList == NULL) return TRUE;
			char segment3[256];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)segment3);
			buffer[4]= atoi(segment3);
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_SpeakerOn:
			//this turns on the speaker
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=215;
			buffer[2]=1;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_SpeakerOff:
			//this turns off the speaker
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=215;
			buffer[2]=0;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_StartCal:
			//starts calibration
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=7;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_SaveCal:
			//saves the calibration
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=9;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		
		case IDC_ShowWheel:
			//shows calibrated wheel data on LED display
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=1;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShowLever1:
			//shows calibrated lever 1 data on LED display
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=2;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShowLever2:
			//shows calibrated lever 2 data on LED display
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=3;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShowSw1:
			//shows calibrated switch 1 data on LED display
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=5;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShowSw2:
			//shows calibrated switch 2 data on LED display
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=195;
			buffer[2]=4;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_WriteUnitID:
			//this writes the unit id entered in edit4 to device
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=189;

			//get text box text
			hList = GetDlgItem(hDialog, IDC_EDIT4);
			if (hList == NULL) return TRUE;
			char UnitID[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)UnitID);
			buffer[2]= atoi(UnitID);
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_KeyboardReflect:
			//Sends keyboard commands as a native keyboard when the 1st key is pressed (see HandleDataEvent for code)
			keyboard=true;
			return TRUE;
		case IDC_JoystickReflect:
			//sends joystick commands
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=202;
			hList = GetDlgItem(hDialog, IDC_EDIT6); //x 0-127=center to full right, 255-128=center to full left
			if (hList == NULL) return TRUE;
			char Data[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[2]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT7); //y 0-127=center to bottom, 255-128=center to top
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[3]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT9); //z rot 0-127=center to bottom, 255-128=center to top
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[4]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT8); //z axis
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[5]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT10); //slider
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[6]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT11); //buttons 1-8
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[7]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT13); //buttons 9-16
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[8]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT14); //buttons 17-24
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[9]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT15); //buttons 25-32
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[10]= atoi(Data);

			hList = GetDlgItem(hDialog, IDC_EDIT12); //point of view hat, 0-7 clockwise, 8=no hat
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)Data);
			buffer[12]= atoi(Data);
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShipModeJoy:
			//analogs send joystick (tiller)
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=216;
			buffer[2]=1; //Joystick: Wheel=Slid. w/wrapping, Lever 1=X, Lever 2=Y, Switch 1=Z Rot, Switch 2=Z Axis
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShipModeJoy3:
			//analogs send joystick (wheel)
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=216;
			buffer[2]=129; //Joystick: Wheel=Slid. w/o wrapping, Lever 1=X, Lever 2=Y, Switch 1=Z Rot, Switch 2=Z Axis
			// data from wheel will not wrap but instead bottom or top out until direction of wheel is reversed
            //the sensitivity determines how many turns of the wheel from one end to the other of the range.

			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_ShipModeNone:
			//analogs send nothing
			//note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=216;
			buffer[2]=0;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_TillerSens:
			//set the sensitivity when in tiller mode
			//note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=219;
			//get text box text
			hList = GetDlgItem(hDialog, IDC_EDIT19);
			if (hList == NULL) return TRUE;
			char sens[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)sens);
			buffer[2]= atoi(sens);
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_WheelSens:
			//set the sensitivity when in wheel mode
			//note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=218;
			//get text box text
			hList = GetDlgItem(hDialog, IDC_EDIT5);
			if (hList == NULL) return TRUE;
			char sensw[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)sensw);
			buffer[2]= atoi(sensw);
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_Deadzone:
			 //Sets the sensitivity of the "deadzone" of the switched.  0 means no deadzone in the middle, higher number are a wider deadzone.
            //note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.
            
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=190;
			//get textbox text
			hList = GetDlgItem(hDialog, IDC_EDIT21);
			if (hList == NULL) return TRUE;
			char sens2[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)sens2);
			buffer[2]= (atoi(sens2)& 0xff);
			buffer[3]= ((atoi(sens2)>>8) & 0xff);
			//get textbox text
			hList = GetDlgItem(hDialog, IDC_EDIT20);
			if (hList == NULL) return TRUE;
			char sens1[10];
			SendMessage(hList, WM_GETTEXT, 8, (LPARAM)sens1);
			buffer[4]= (atoi(sens1)& 0xff);
			buffer[5]= ((atoi(sens1)>>8) & 0xff);
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_WheelZero:
			//zero wheel when in wheel mode
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=217;
			
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_MacrosOn:
			//If there are internal macros stored, make so will be executed on key presses.
            //Unit will default back to Macros On if hotplugged or rebooted
            //These macros programmed using the hardware mode feature of the MacroWorks 3 software
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=211;
			//enable byte is bit 7=0, bit 6=0, bit 5=0, bit 4=Splat, bit 3=Mouse (N/A), bit 2=Joy, bit 1=Keyboard, bit 0=Stored Macros
			buffer[2]=23; //default is 23
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_MacrosOff:
			 //If there are internal macros stored, make so will not be executed on key presses.
            //Unit will default back to Macros On if hotplugged or rebooted
            //These macros programmed using the hardware mode feature of the MacroWorks 3 software
			memset(buffer, 0, 80);
			for ( i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[1]=211;
			//enable byte is bit 7=0, bit 6=0, bit 5=0, bit 4=Splat, bit 3=Mouse (N/A), bit 2=Joy, bit 1=Keyboard, bit 0=Stored Macros
			buffer[2]=22; //default is 23
			result = WriteData(hDevice, buffer);
			return TRUE;
		case IDC_StartMyJoystick:
			//Start the polling timer to read incoming data from port
			SetTimer(hwndDlg, 0, 100, DlgTimerProc);  
			return TRUE;
		case IDC_StopMyJoystick:	
			KillTimer(hwndDlg,0);
			return TRUE;
		case IDC_Center:
			//if controlling completly the analogs using Joystick Reflector command this centers the wheel in the middle of the range if No Wrap is checked.
			wheel=128;
			triggerit=true;
			return TRUE;
		case IDC_Descriptor:
			//turn off the callback
			SendMessage(GetDlgItem(hwndDlg, CHK_NONE), BM_SETCHECK, BST_CHECKED, 0);
			SendMessage(GetDlgItem(hwndDlg, CHK_NEW), BM_SETCHECK, BST_UNCHECKED, 0);
			if (hDevice == -1) return TRUE;

			DisableDataCallback(hDevice, true); //turn off callback so capture data here
		
			if (result != 0)    {
				AddEventMsg(hwndDlg, "Error setting event callback");
			}

			for (i=0;i<writelen;i++)
			{
				buffer[i]=0;
			}
			buffer[0]=0;
			buffer[1]=214;
			result = WriteData(hDevice, buffer);
			//after this write the next read 3rd byte=214 will give descriptor information
			for (i=0;i<80;i++)
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
				if (pid==0x41f)
				{
					AddEventMsg(hDialog, "Found Device: ShipDriver");
				}
				
				else
				{
					AddEventMsg(hDialog, "Unknown device found");
				}
				return;
			}
		
		}
	}

	AddEventMsg(hDialog, "No X-keys devices found");
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

	sprintf_s(dataStr, "%02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x -- %02x %02x %02x %02x\n", 
		pData[0], pData[1], pData[2], pData[3], pData[4], pData[5], pData[6], pData[7], pData[8], pData[9], pData[10], pData[11], pData[12], pData[13], pData[14], pData[15], pData[16], pData[17], pData[18], pData[19], pData[20], pData[21], pData[22], pData[23]);

	AddEventMsg(hDialog, dataStr);

	//for Keyboard Reflect, 
		if ((pData[3]&1) && keyboard==true)
		{
		//Sends keyboard commands as a native keyboard
		//Before pressing 1st key activate a Notepad or other text capturing application to see the results: Abcd
		int wlen=GetWriteLength(hDevice);
		for(int i=0;i<wlen;i++)
		{
			buffer[i]=0;
		}
		MessageBeep(MB_ICONHAND);
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
		WriteData(hDevice, buffer);

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
		WriteData(hDevice, buffer);

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
		WriteData(hDevice, buffer);
		keyboard=false;
		}

		//Wheel stuff
		float theta = pData[11] * 256.0 + pData[12];
        theta = (theta / (256.0 * 256.0)) * 360.0;
        theta = 360 - theta;
		int thetai=theta; //convert to int
        HWND hList;
		hList = GetDlgItem(hDialog, IDC_EDIT16);
		if (hList == NULL) return TRUE;
		_itoa_s(thetai,dataStr,10);
		SendMessage(hList, WM_SETTEXT, 0, (LPARAM)dataStr);

		//find direction of wheel
		float delta=theta-lasttheta;
		int halfrange = 180;
        int fullrange = 360;
        if (delta < -1 * halfrange)
			delta = delta + fullrange;
        else if (delta > halfrange)
            delta = delta - fullrange;
		if (delta>0) //clockwise
		{
			hList = GetDlgItem(hDialog, IDC_EDIT17);
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"clockwise");
		}
		else //counter clockwise
		{
			hList = GetDlgItem(hDialog, IDC_EDIT17);
			if (hList == NULL) return TRUE;
			SendMessage(hList, WM_SETTEXT, 0, (LPARAM)"counter clockwise");
		}
		lasttheta=theta;

		//limit the wheel data to desired bounds, -180 to 180 in this sample
		if (firstread == false)
        {
			runningtotal = runningtotal + delta;
            if (runningtotal > 180)
				runningtotal = 180;
            if (runningtotal < -180)
                runningtotal = -180;
			int runningtotali=runningtotal; //convert to int
			hList = GetDlgItem(hDialog, IDC_EDIT18);
			if (hList == NULL) return TRUE;
			_itoa_s(runningtotali,dataStr,10);
			SendMessage(hList, WM_SETTEXT, 0, (LPARAM)dataStr);
        }
		firstread=false;
	
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