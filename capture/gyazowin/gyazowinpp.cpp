// gyazowin.cpp : �A�v���P�[�V�����̃G���g�� �|�C���g���`���܂��B
//

#include "stdafx.h"
#include "gyazowinpp.h"

// �}�N����`
#define UPLOAD_LOG_LINE_SIZE	(128)				// ���O�̏������݃T�C�Y�i�{��89byte�ŏ\�������o�b�t�@�Ƃ��Ċm�ہj
#define	APPLICATION_NAME		"gyazowinpp"		// �A�v���P�[�V������
#define INIFILE_NAME			"gyazowinpp.ini"	// ini�t�@�C����
#define INIFILE_SECTION_NAME	APPLICATION_NAME	// ini�t�@�C���Z�N�V������

// �O���[�o���ϐ�:
HINSTANCE hInst;									// ���݂̃C���^�[�t�F�C�X
TCHAR *szTitle			= _T(APPLICATION_NAME);		// �^�C�g�� �o�[�̃e�L�X�g
TCHAR *szWindowClass	= _T("GYAZOWIN");			// ���C�� �E�B���h�E �N���X��
TCHAR *szWindowClassL	= _T("GYAZOWINL");			// ���C���[ �E�B���h�E �N���X��
HWND hLayerWnd;

int ofX, ofY;	// ��ʃI�t�Z�b�g
std::map<std::wstring, std::wstring> g_Settings;

// �v���g�^�C�v�錾
ATOM				MyRegisterClass(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK	LayerWndProc(HWND, UINT, WPARAM, LPARAM);

int					GetEncoderClsid(const WCHAR* format, CLSID* pClsid);

BOOL				isPng(LPCTSTR fileName);
VOID				drawRubberband(HDC hdc, LPRECT newRect, BOOL erase);
VOID				execUrl(const char* str);
VOID				setClipBoardText(const char* str);
BOOL				convertPNG(LPCTSTR destFile, LPCTSTR srcFile);
BOOL				savePNG(LPCTSTR fileName, HBITMAP newBMP);
BOOL				uploadFile(HWND hwnd, LPCTSTR fileName);
std::string			getId();
std::map<std::wstring, std::wstring> loadSettings(LPCWSTR fileName, LPCWSTR sectionName);
BOOL				writeLog(const char*, const char*);

// �G���g���[�|�C���g
int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	MSG msg;

	// �J�����g�f�B���N�g��AppData�ɂ���
	TCHAR idDir[_MAX_PATH];
	SHGetSpecialFolderPath( NULL, idDir, CSIDL_APPDATA, FALSE );
	 _tcscat_s( idDir, _T("\\GyazoHJ"));
	SetCurrentDirectory(idDir);

	g_Settings = loadSettings(_T(INIFILE_NAME), _T(INIFILE_SECTION_NAME));

	// �����Ƀt�@�C�����w�肳��Ă�����
	if ( 2 == __argc )
	{
		// �t�@�C�����A�b�v���[�h���ďI��
		if (isPng(__targv[1])) {
			// PNG �͂��̂܂�upload
			uploadFile(NULL, __targv[1]);
		}else {
			// PNG �`���ɕϊ�
			TCHAR tmpDir[MAX_PATH], tmpFile[MAX_PATH];
			GetTempPath(MAX_PATH, tmpDir);
			GetTempFileName(tmpDir, _T("gya"), 0, tmpFile);
			
			if (convertPNG(tmpFile, __targv[1])) {
				//�A�b�v���[�h
				uploadFile(NULL, tmpFile);
			} else {
				// PNG�ɕϊ��ł��Ȃ�����...
				MessageBox(NULL, _T("Cannot convert this image"), szTitle, 
					MB_OK | MB_ICONERROR);
			}
			DeleteFile(tmpFile);
		}
		return TRUE;
	}

	// �E�B���h�E�N���X��o�^
	MyRegisterClass(hInstance);

	// �A�v���P�[�V�����̏����������s���܂�:
	if (!InitInstance (hInstance, nCmdShow))
	{
		return FALSE;
	}
	
	// ���C�� ���b�Z�[�W ���[�v:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return (int) msg.wParam;
}

// �ݒ�����[�h����
std::map<std::wstring, std::wstring> loadSettings(LPCWSTR fileName, LPCWSTR sectionName)
{
	std::map<std::wstring, std::wstring> settings;
	TCHAR wcFilePath[MAX_PATH];
	TCHAR wcSettings[32767];
	TCHAR *lpwcCurrent;
	TCHAR *lpwcString;
	TCHAR *lpwcContext;
	size_t len;
	std::wstring key;
	std::wstring value;

	GetFullPathName(fileName, MAX_PATH, wcFilePath, NULL);
	GetPrivateProfileSection(sectionName, wcSettings, 32767, wcFilePath);

	lpwcCurrent = wcSettings;
	while (*lpwcCurrent) {
		len = wcslen(lpwcCurrent);
		lpwcString = wcstok_s(lpwcCurrent, _T("="), &lpwcContext);
		key = std::wstring(lpwcString);
		value = std::wstring(lpwcString + wcslen(lpwcString) + 1);
		settings[key] = value;
		lpwcCurrent += len + 1;
	}

	return settings;
}

// �w�b�_������ PNG �摜���ǂ���(�ꉞ)�`�F�b�N
BOOL isPng(LPCTSTR fileName)
{
	unsigned char pngHead[] = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
	unsigned char readHead[8];
	
	FILE *fp = NULL;
	
	if (0 != _tfopen_s(&fp, fileName, _T("rb")) ||
		8 != fread(readHead, 1, 8, fp)) {
		// �t�@�C�����ǂ߂Ȃ�	
		return FALSE;
	}
	fclose(fp);
	
	// compare
	for(unsigned int i=0;i<8;i++)
		if(pngHead[i] != readHead[i]) return FALSE;

	return TRUE;

}

// �E�B���h�E�N���X��o�^
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASS wc;

	// ���C���E�B���h�E
	wc.style         = 0;							// WM_PAINT �𑗂�Ȃ�
	wc.lpfnWndProc   = WndProc;
	wc.cbClsExtra    = 0;
	wc.cbWndExtra    = 0;
	wc.hInstance     = hInstance;
	wc.hIcon         = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_GYAZOWIN));
	wc.hCursor       = LoadCursor(NULL, IDC_CROSS);	// + �̃J�[�\��
	wc.hbrBackground = 0;							// �w�i���ݒ肵�Ȃ�
	wc.lpszMenuName  = 0;
	wc.lpszClassName = szWindowClass;

	RegisterClass(&wc);

	// ���C���[�E�B���h�E
	wc.style         = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc   = LayerWndProc;
	wc.cbClsExtra    = 0;
	wc.cbWndExtra    = 0;
	wc.hInstance     = hInstance;
	wc.hIcon         = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_GYAZOWIN));
	wc.hCursor       = LoadCursor(NULL, IDC_CROSS);	// + �̃J�[�\��
	wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	wc.lpszMenuName  = 0;
	wc.lpszClassName = szWindowClassL;

	return RegisterClass(&wc);
}


// �C���X�^���X�̏������i�S��ʂ��E�B���h�E�ŕ����j
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;
//	HWND hLayerWnd;
	hInst = hInstance; // �O���[�o���ϐ��ɃC���X�^���X�������i�[���܂��B

	int x, y, w, h;

	// ���z�X�N���[���S�̂��J�o�[
	x = GetSystemMetrics(SM_XVIRTUALSCREEN);
	y = GetSystemMetrics(SM_YVIRTUALSCREEN);
	w = GetSystemMetrics(SM_CXVIRTUALSCREEN);
	h = GetSystemMetrics(SM_CYVIRTUALSCREEN);

	// x, y �̃I�t�Z�b�g�l���o���Ă���
	ofX = x; ofY = y;

	// ���S�ɓ��߂����E�B���h�E�����
	hWnd = CreateWindowEx(
		WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW | WS_EX_TOPMOST
#if(_WIN32_WINNT >= 0x0500)
		| WS_EX_NOACTIVATE
#endif
		,
		szWindowClass, NULL, WS_POPUP,
		0, 0, 0, 0,
		NULL, NULL, hInstance, NULL);

	// ���Ȃ�����...?
	if (!hWnd) return FALSE;
	
	// �S��ʂ𕢂�
	MoveWindow(hWnd, x, y, w, h, FALSE);
	
	// nCmdShow �𖳎� (SW_MAXIMIZE �Ƃ������ƍ���)
	ShowWindow(hWnd, SW_SHOW);
	UpdateWindow(hWnd);

	// ESC�L�[���m�^�C�}�[
	SetTimer(hWnd, 1, 100, NULL);


	// ���C���[�E�B���h�E�̍쐬
	hLayerWnd = CreateWindowEx(
	 WS_EX_TOOLWINDOW
#if(_WIN32_WINNT >= 0x0500)
		| WS_EX_LAYERED | WS_EX_NOACTIVATE
#endif
		,
		szWindowClassL, NULL, WS_POPUP,
		100, 100, 300, 300,
		hWnd, NULL, hInstance, NULL);

    SetLayeredWindowAttributes(hLayerWnd, RGB(255, 0, 0), 100, LWA_COLORKEY|LWA_ALPHA);

	


	
	return TRUE;
}

// �w�肳�ꂽ�t�H�[�}�b�g�ɑΉ����� Encoder �� CLSID ���擾����
// Cited from MSDN Library: Retrieving the Class Identifier for an Encoder
int GetEncoderClsid(const WCHAR* format, CLSID* pClsid)
{
   UINT  num = 0;          // number of image encoders
   UINT  size = 0;         // size of the image encoder array in bytes

   ImageCodecInfo* pImageCodecInfo = NULL;

   GetImageEncodersSize(&num, &size);
   if(size == 0)
      return -1;  // Failure

   pImageCodecInfo = (ImageCodecInfo*)(malloc(size));
   if(pImageCodecInfo == NULL)
      return -1;  // Failure

   GetImageEncoders(num, size, pImageCodecInfo);

   for(UINT j = 0; j < num; ++j)
   {
      if( wcscmp(pImageCodecInfo[j].MimeType, format) == 0 )
      {
         *pClsid = pImageCodecInfo[j].Clsid;
         free(pImageCodecInfo);
         return j;  // Success
      }    
   }

   free(pImageCodecInfo);
   return -1;  // Failure
}

// ���o�[�o���h��`��.
VOID drawRubberband(HDC hdc, LPRECT newRect, BOOL erase)
{
	
	static BOOL firstDraw = TRUE;	// 1 ��ڂ͑O�̃o���h�̏������s��Ȃ�
	static RECT lastRect  = {0};	// �Ō�ɕ`�悵���o���h
	static RECT clipRect  = {0};	// �Ō�ɕ`�悵���o���h
	
	if(firstDraw) {
		// ���C���[�E�B���h�E��\��
		ShowWindow(hLayerWnd, SW_SHOW);
		UpdateWindow(hLayerWnd);

		firstDraw = FALSE;
	}

	if (erase) {
		// ���C���[�E�B���h�E���B��
		ShowWindow(hLayerWnd, SW_HIDE);
		
	}

	// ���W�`�F�b�N
	clipRect = *newRect;
	if ( clipRect.right  < clipRect.left ) {
		int tmp = clipRect.left;
		clipRect.left   = clipRect.right;
		clipRect.right  = tmp;
	}
	if ( clipRect.bottom < clipRect.top  ) {
		int tmp = clipRect.top;
		clipRect.top    = clipRect.bottom;
		clipRect.bottom = tmp;
	}
	MoveWindow(hLayerWnd,  clipRect.left, clipRect.top, 
			clipRect.right-  clipRect.left + 1, clipRect.bottom - clipRect.top + 1,true);

	
	return;

/* rakusai 2009/11/2

	// XOR �ŕ`��
	int hPreRop = SetROP2(hdc, R2_XORPEN);

	// �_��
	HPEN hPen = CreatePen(PS_DOT , 1, 0);
	SelectObject(hdc, hPen);
	SelectObject(hdc, GetStockObject(NULL_BRUSH));

	if(!firstDraw) {
		// �O�̂�����
		Rectangle(hdc, lastRect.left, lastRect.top, 
			lastRect.right + 1, lastRect.bottom + 1);
	} else {
		firstDraw = FALSE;
	}
	
	// �V�������W���L��
	lastRect = *newRect;
	
	


	if (!erase) {

		// �g��`��
		Rectangle(hdc, lastRect.left, lastRect.top, 
			lastRect.right + 1, lastRect.bottom + 1);

	}


	// �㏈��
	SetROP2(hdc, hPreRop);
	DeleteObject(hPen);

*/

}

// PNG �`���ɕϊ�
BOOL convertPNG(LPCTSTR destFile, LPCTSTR srcFile)
{
	BOOL				res = FALSE;

	GdiplusStartupInput	gdiplusStartupInput;
	ULONG_PTR			gdiplusToken;
	CLSID				clsidEncoder;

	// GDI+ �̏�����
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	Image *b = new Image(srcFile, 0);

	if (0 == b->GetLastStatus()) {
		if (GetEncoderClsid(L"image/png", &clsidEncoder)) {
			// save!
			if (0 == b->Save(destFile, &clsidEncoder, 0) ) {
					// �ۑ��ł���
					res = TRUE;
			}
		}
	}

	// ��n��
	delete b;
	GdiplusShutdown(gdiplusToken);

	return res;
}

// PNG �`���ŕۑ� (GDI+ �g�p)
BOOL savePNG(LPCTSTR fileName, HBITMAP newBMP)
{
	BOOL				res = FALSE;

	GdiplusStartupInput	gdiplusStartupInput;
	ULONG_PTR			gdiplusToken;
	CLSID				clsidEncoder;

	// GDI+ �̏�����
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
	
	// HBITMAP ���� Bitmap ���쐬
	Bitmap *b = new Bitmap(newBMP, NULL);
	
	if (GetEncoderClsid(L"image/png", &clsidEncoder)) {
		// save!
		if (0 ==
			b->Save(fileName, &clsidEncoder, 0) ) {
				// �ۑ��ł���
				res = TRUE;
		}
	}
	
	// ��n��
	delete b;
	GdiplusShutdown(gdiplusToken);

	return res;
}

// ���C���[�E�B���h�E�v���V�[�W��
LRESULT CALLBACK LayerWndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	HDC hdc;
	RECT clipRect	= {0, 0, 500, 500};
	HBRUSH hBrush;
	HPEN hPen;
	HFONT hFont;


	switch (message)
	{
	case WM_ERASEBKGND:
		 GetClientRect(hWnd, &clipRect);
		
		hdc = GetDC(hWnd);
        hBrush = CreateSolidBrush(RGB(100,100,100));
        SelectObject(hdc, hBrush);
		hPen = CreatePen(PS_DASH,1,RGB(255,255,255));
		SelectObject(hdc, hPen);
		Rectangle(hdc,0,0,clipRect.right,clipRect.bottom);

		//��`�̃T�C�Y���o��
		int fHeight;
		fHeight = -MulDiv(8, GetDeviceCaps(hdc, LOGPIXELSY), 72);
		hFont = CreateFont(fHeight,    //�t�H���g����
			0,                    //������
			0,                    //�e�L�X�g�̊p�x
			0,                    //�x�[�X���C���Ƃ����Ƃ̊p�x
			FW_REGULAR,            //�t�H���g�̏d���i�����j
			FALSE,                //�C�^���b�N��
			FALSE,                //�A���_�[���C��
			FALSE,                //�ł�������
			ANSI_CHARSET,    //�����Z�b�g
			OUT_DEFAULT_PRECIS,    //�o�͐��x
			CLIP_DEFAULT_PRECIS,//�N���b�s���O���x
			PROOF_QUALITY,        //�o�͕i��
			FIXED_PITCH | FF_MODERN,//�s�b�`�ƃt�@�~���[
			L"Tahoma");    //���̖�

		SelectObject(hdc, hFont);
		// show size
		int iWidth, iHeight;
		iWidth  = clipRect.right  - clipRect.left;
		iHeight = clipRect.bottom - clipRect.top;

		wchar_t sWidth[200], sHeight[200];
		swprintf_s(sWidth, L"%d", iWidth);
		swprintf_s(sHeight, L"%d", iHeight);

		int w,h,h2;
		w = -fHeight * 2.5 + 8;
		h = -fHeight * 2 + 8;
		h2 = h + fHeight;

		SetBkMode(hdc,TRANSPARENT);
		SetTextColor(hdc,RGB(0,0,0));
		TextOut(hdc, clipRect.right-w+1,clipRect.bottom-h+1,(LPCWSTR)sWidth,wcslen(sWidth));
		TextOut(hdc, clipRect.right-w+1,clipRect.bottom-h2+1,(LPCWSTR)sHeight,wcslen(sHeight));
		SetTextColor(hdc,RGB(255,255,255));
		TextOut(hdc, clipRect.right-w,clipRect.bottom-h,(LPCWSTR)sWidth,wcslen(sWidth));
		TextOut(hdc, clipRect.right-w,clipRect.bottom-h2,(LPCWSTR)sHeight,wcslen(sHeight));

		DeleteObject(hPen);
		DeleteObject(hBrush);
		DeleteObject(hFont);
		ReleaseDC(hWnd, hdc);

		return TRUE;

        break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;

}

// �E�B���h�E�v���V�[�W��
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	HDC hdc;
	
	static BOOL onClip		= FALSE;
	static BOOL firstDraw	= TRUE;
	static RECT clipRect	= {0, 0, 0, 0};
	
	switch (message)
	{
	case WM_RBUTTONDOWN:
		// �L�����Z��
		DestroyWindow(hWnd);
		return DefWindowProc(hWnd, message, wParam, lParam);
		break;

	case WM_TIMER:
		// ESC�L�[�����̌��m
		if (GetKeyState(VK_ESCAPE) & 0x8000){
			DestroyWindow(hWnd);
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;

	case WM_MOUSEMOVE:
		if (onClip) {
			// �V�������W���Z�b�g
			clipRect.right  = LOWORD(lParam) + ofX;
			clipRect.bottom = HIWORD(lParam) + ofY;
			
			hdc = GetDC(NULL);
			drawRubberband(hdc, &clipRect, FALSE);

			ReleaseDC(NULL, hdc);
		}
		break;
	

	case WM_LBUTTONDOWN:
		{
			// �N���b�v�J�n
			onClip = TRUE;
			
			// �����ʒu���Z�b�g
			clipRect.left = LOWORD(lParam) + ofX;
			clipRect.top  = HIWORD(lParam) + ofY;
			


			// �}�E�X���L���v�`��
			SetCapture(hWnd);
		}
		break;

	case WM_LBUTTONUP:
		{
			// �N���b�v�I��
			onClip = FALSE;
			
			// �}�E�X�̃L���v�`��������
			ReleaseCapture();
		
			// �V�������W���Z�b�g
			clipRect.right  = LOWORD(lParam) + ofX;
			clipRect.bottom = HIWORD(lParam) + ofY;

			// ��ʂɒ��ڕ`��C���Č`
			HDC hdc = GetDC(NULL);

			// ��������
			drawRubberband(hdc, &clipRect, TRUE);

			// ���W�`�F�b�N
			if ( clipRect.right  < clipRect.left ) {
				int tmp = clipRect.left;
				clipRect.left   = clipRect.right;
				clipRect.right  = tmp;
			}
			if ( clipRect.bottom < clipRect.top  ) {
				int tmp = clipRect.top;
				clipRect.top    = clipRect.bottom;
				clipRect.bottom = tmp;
			}
			
			// �摜�̃L���v�`��
			int iWidth, iHeight;
			iWidth  = clipRect.right  - clipRect.left + 1;
			iHeight = clipRect.bottom - clipRect.top  + 1;

			if(iWidth == 0 || iHeight == 0) {
				// �摜�ɂȂ��ĂȂ�, �Ȃɂ����Ȃ�
				ReleaseDC(NULL, hdc);
				DestroyWindow(hWnd);
				break;
			}

			// �r�b�g�}�b�v�o�b�t�@���쐬
			HBITMAP newBMP = CreateCompatibleBitmap(hdc, iWidth, iHeight);
			HDC	    newDC  = CreateCompatibleDC(hdc);
			
			// �֘A�Â�
			SelectObject(newDC, newBMP);

			// �摜���擾
			BitBlt(newDC, 0, 0, iWidth, iHeight, 
				hdc, clipRect.left, clipRect.top, SRCCOPY);
			
			// �E�B���h�E���B��!
			ShowWindow(hWnd, SW_HIDE);
			/*
			// �摜���N���b�v�{�[�h�ɃR�s�[
			if ( OpenClipboard(hWnd) ) {
				// ����
				EmptyClipboard();
				// �Z�b�g
				SetClipboardData(CF_BITMAP, newBMP);
				// ����
				CloseClipboard();
			}
			*/
			
			// �e���|�����t�@�C����������
			TCHAR tmpDir[MAX_PATH], tmpFile[MAX_PATH];
			GetTempPath(MAX_PATH, tmpDir);
			GetTempFileName(tmpDir, _T("gya"), 0, tmpFile);
			
			if (savePNG(tmpFile, newBMP)) {

				// ����
				if (!uploadFile(hWnd, tmpFile)) {
					// �A�b�v���[�h�Ɏ��s...
					// �G���[���b�Z�[�W�͊��ɕ\������Ă���

					/*
					TCHAR sysDir[MAX_PATH];
					if (SUCCEEDED(StringCchCopy(sysDir, MAX_PATH, tmpFile)) &&
						SUCCEEDED(StringCchCat(sysDir, MAX_PATH, _T(".png")))) {
						
						MoveFile(tmpFile, sysDir);
						SHELLEXECUTEINFO lsw = {0};
						
						lsw.hwnd	= hWnd;
						lsw.cbSize	= sizeof(SHELLEXECUTEINFO);
						lsw.lpVerb	= _T("open");
						lsw.lpFile	= sysDir;

						ShellExecuteEx(&lsw);
					}
					*/
				}
			} else {
				// PNG�ۑ����s...
				MessageBox(hWnd, _T("Cannot save png image"), szTitle, 
					MB_OK | MB_ICONERROR);
			}

			// ��n��
			DeleteFile(tmpFile);
			
			DeleteDC(newDC);
			DeleteObject(newBMP);

			ReleaseDC(NULL, hdc);
			DestroyWindow(hWnd);
			PostQuitMessage(0);
		}
		break;

	case WM_DESTROY:
		PostQuitMessage(0);
		break;

	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// �N���b�v�{�[�h�ɕ�������R�s�[
VOID setClipBoardText(const char* str)
{

	HGLOBAL hText;
	char    *pText;
	size_t  slen;

	slen  = strlen(str) + 1; // NULL

	hText = GlobalAlloc(GMEM_DDESHARE | GMEM_MOVEABLE, slen * sizeof(TCHAR));

	pText = (char *)GlobalLock(hText);
	strncpy_s(pText, slen, str, slen);
	GlobalUnlock(hText);
	
	// �N���b�v�{�[�h���J��
	OpenClipboard(NULL);
	EmptyClipboard();
	SetClipboardData(CF_TEXT, hText);
	CloseClipboard();

	// ���
	GlobalFree(hText);
}

// �w�肳�ꂽ URL (char*) ���u���E�U�ŊJ��
VOID execUrl(const char* str)
{
	size_t  slen;
	size_t  dcount;
	slen  = strlen(str) + 1; // NULL

	TCHAR *wcUrl = (TCHAR *)malloc(slen * sizeof(TCHAR));
	
	// ���C�h�����ɕϊ�
	mbstowcs_s(&dcount, wcUrl, slen, str, slen);
	
	// open �R�}���h�����s
	SHELLEXECUTEINFO lsw = {0};
	lsw.cbSize = sizeof(SHELLEXECUTEINFO);
	lsw.lpVerb = _T("open");
	lsw.lpFile = wcUrl;

	ShellExecuteEx(&lsw);

	free(wcUrl);
}

// ID �𐶐��E���[�h����
std::string getId()
{

    TCHAR idFile[_MAX_PATH];
	TCHAR idDir[_MAX_PATH];

    SHGetSpecialFolderPath( NULL, idFile, CSIDL_APPDATA, FALSE );

	 _tcscat_s( idFile, _T("\\GyazoHJ"));
	 _tcscpy_s( idDir, idFile);
	 _tcscat_s( idFile, _T("\\id.txt"));

	std::string idStr;

	// �܂��̓t�@�C������ ID �����[�h
	std::ifstream ifs;

	ifs.open(idFile);
	if (! ifs.fail()) {
		// ID ��ǂݍ���
		ifs >> idStr;
		ifs.close();
	}

	return idStr;
}

// Save ID
BOOL saveId(const WCHAR* str)
{

    TCHAR idFile[_MAX_PATH];
	TCHAR idDir[_MAX_PATH];

    SHGetSpecialFolderPath( NULL, idFile, CSIDL_APPDATA, FALSE );

	 _tcscat_s( idFile, _T("\\GyazoHJ"));
	 _tcscpy_s( idDir, idFile);
	 _tcscat_s( idFile, _T("\\id.txt"));

	size_t  slen;
	size_t  dcount;
	slen  = _tcslen(str) + 1; // NULL

	char *idStr = (char *)malloc(slen * sizeof(char));
	// �o�C�g�����ɕϊ�
	wcstombs_s(&dcount, idStr, slen, str, slen);

	// ID ��ۑ�����
	CreateDirectory(idDir,NULL);
	std::ofstream ofs;
	ofs.open(idFile);
	if (! ofs.fail()) {
		ofs << idStr;
		ofs.close();
	}else{
		free(idStr);
		return FALSE;
	}

	free(idStr);
	return TRUE;
}

// PNG �t�@�C�����A�b�v���[�h����.
BOOL uploadFile(HWND hwnd, LPCTSTR fileName)
{
//	const TCHAR* UPLOAD_SERVER	= _T("gyazo.com");
//	const TCHAR* UPLOAD_PATH	= _T("/upload.cgi");

	const char*  sBoundary = "----BOUNDARYBOUNDARY----";		// boundary
	const char   sCrLf[]   = { 0xd, 0xa, 0x0 };					// ���s(CR+LF)
	const TCHAR* szHeader  = 
		_T("Content-type: multipart/form-data; boundary=----BOUNDARYBOUNDARY----");

	std::ostringstream	buf;	// ���M���b�Z�[�W
	std::string			idStr;	// ID

	LPCWSTR lpwcUploadServer;	// �A�b�v���[�h��T�[�o
	LPCWSTR lpwcUploadPath;		// �A�b�v���[�h��p�X

	LPCWSTR lpwcId;			// �F�ؗpID
	LPCWSTR lpwcPassword;	// �F�ؗp�p�X���[�h

	DWORD dwFlags;	// �t���O

	// �A�b�v���[�h�m�F
	if (g_Settings.count(L"up_dialog") && g_Settings[L"up_dialog"] == L"yes") {
		if (MessageBox(hwnd,_T("�A�b�v���[�h���܂����H"),_T("Question"),MB_OK|MB_ICONQUESTION|MB_YESNO) != IDYES) {
			return TRUE;
		}
	}

	// ID ���擾
	idStr = getId();

	// ���b�Z�[�W�̍\��
	// -- "id" part
	buf << "--";
	buf << sBoundary;
	buf << sCrLf;
	buf << "content-disposition: form-data; name=\"id\"";
	buf << sCrLf;
	buf << sCrLf;
	buf << idStr;
	buf << sCrLf;

	// -- "imagedata" part
	buf << "--";
	buf << sBoundary;
	buf << sCrLf;
	buf << "content-disposition: form-data; name=\"imagedata\"; filename=\"data.png\"";
	buf << sCrLf;
	//buf << "Content-type: image/png";	// �ꉞ
	//buf << sCrLf;
	buf << sCrLf;

	// �{��: PNG �t�@�C����ǂݍ���
	std::ifstream png;
	png.open(fileName, std::ios::binary);
	if (png.fail()) {
		MessageBox(hwnd, _T("PNG open failed"), szTitle, MB_ICONERROR | MB_OK);
		png.close();
		return FALSE;
	}
	buf << png.rdbuf();		// read all & append to buffer
	png.close();

	// �Ō�
	buf << sCrLf;
	buf << "--";
	buf << sBoundary;
	buf << "--";
	buf << sCrLf;

	// ���b�Z�[�W����
	std::string oMsg(buf.str());

	// �A�b�v���[�h��
	if (g_Settings.count(L"upload_server")) {
		lpwcUploadServer = g_Settings[L"upload_server"].c_str();
	}else{
		lpwcUploadServer = L"gyazo.com";
	}
	if (g_Settings.count(L"upload_path")) {
		lpwcUploadPath = g_Settings[L"upload_path"].c_str();
	}else{
		lpwcUploadPath = L"/upload.cgi";
	}

	// �F�؃f�[�^����
	if (g_Settings.count(L"use_auth") && g_Settings[L"use_auth"] == L"yes") {
		if (g_Settings.count(L"auth_id")) {
			lpwcId = g_Settings[L"auth_id"].c_str();
		}else{
			lpwcId = L"";
		}
		if (g_Settings.count(L"auth_pw")) {
			lpwcPassword = g_Settings[L"auth_pw"].c_str();
		}else{
			lpwcPassword = L"";
		}
	}else{
		lpwcId = NULL;
		lpwcPassword = NULL;
	}

	// WinInet ������ (proxy �� �K��̐ݒ�𗘗p)
	HINTERNET hSession    = InternetOpen(szTitle, 
		INTERNET_OPEN_TYPE_PRECONFIG, NULL, NULL, 0);
	if(NULL == hSession) {
		MessageBox(hwnd, _T("Cannot configure wininet"),
			szTitle, MB_ICONERROR | MB_OK);
		return FALSE;
	}

	// SSL
	dwFlags = INTERNET_FLAG_DONT_CACHE | INTERNET_FLAG_RELOAD;
	if (g_Settings.count(L"use_ssl") && g_Settings[L"use_ssl"] == L"yes") {
		dwFlags |= INTERNET_FLAG_SECURE;
		if (g_Settings.count(L"ssl_check_cert") && g_Settings[L"ssl_check_cert"] == L"no") {
			dwFlags |= INTERNET_FLAG_IGNORE_CERT_CN_INVALID | INTERNET_FLAG_IGNORE_CERT_DATE_INVALID;
		}
	}

	// �ڑ���
	HINTERNET hConnection = InternetConnect(hSession, 
		lpwcUploadServer, INTERNET_DEFAULT_HTTP_PORT,
		lpwcId, lpwcPassword, INTERNET_SERVICE_HTTP, 0, NULL);
	if(NULL == hSession) {
		MessageBox(hwnd, _T("Cannot initiate connection"),
			szTitle, MB_ICONERROR | MB_OK);
		return FALSE;
	}

	// �v����̐ݒ�
	HINTERNET hRequest    = HttpOpenRequest(hConnection,
		_T("POST"), lpwcUploadPath, NULL,
		NULL, NULL, dwFlags, NULL);
	if(NULL == hSession) {
		MessageBox(hwnd, _T("Cannot compose post request"),
			szTitle, MB_ICONERROR | MB_OK);
		return FALSE;
	}

	// User-Agent���w��
	const TCHAR* ua = _T("User-Agent: Gyazowin/1.0\r\n");
	BOOL bResult = HttpAddRequestHeaders(
		hRequest, ua, _tcslen(ua), 
		HTTP_ADDREQ_FLAG_ADD | HTTP_ADDREQ_FLAG_REPLACE);
	if (FALSE == bResult) {
		MessageBox(hwnd, _T("Cannot set user agent"),
			szTitle, MB_ICONERROR | MB_OK);
		return FALSE;
	}
	
	// �v���𑗐M
	if (HttpSendRequest(hRequest,
                    szHeader,
					lstrlen(szHeader),
                    (LPVOID)oMsg.c_str(),
					(DWORD) oMsg.length()))
	{
		// �v���͐���
		
		DWORD resLen = 8;
		TCHAR resCode[8];

		// status code ���擾
		HttpQueryInfo(hRequest, HTTP_QUERY_STATUS_CODE, resCode, &resLen, 0);
		if( _ttoi(resCode) != 200 ) {
			// upload ���s (status error)
			MessageBox(hwnd, _T("Failed to upload (unexpected result code, under maintainance?)"),
				szTitle, MB_ICONERROR | MB_OK);
		} else {
			// upload succeeded

			// get new id
			DWORD idLen = 100;
			TCHAR newid[100];
			
			memset(newid, 0, idLen*sizeof(TCHAR));	
			_tcscpy_s(newid, _T("X-Gyazo-Id"));

			HttpQueryInfo(hRequest, HTTP_QUERY_CUSTOM, newid, &idLen, 0);
			if (GetLastError() != ERROR_HTTP_HEADER_NOT_FOUND && idLen != 0) {
				//save new id
				saveId(newid);
			}

			// ���� (URL) ��ǎ��
			DWORD len;
			char  resbuf[1024];
			std::string result;
			
			// ����Ȃɒ������Ƃ͂Ȃ����ǂ܂��ꉞ
			while(InternetReadFile(hRequest, (LPVOID) resbuf, 1024, &len) 
				&& len != 0)
			{
				result.append(resbuf, len);
			}

			// �擾���ʂ� NULL terminate ����Ă��Ȃ��̂�
			result += '\0';

			// �N���b�v�{�[�h�� URL ���R�s�[
			if (g_Settings.count(L"copy_url") && g_Settings[L"copy_url"] == L"yes") {
				setClipBoardText(result.c_str());
				if (g_Settings.count(L"copy_dialog") && g_Settings[L"copy_dialog"] == L"yes") {
					MessageBox(hwnd,_T("�N���b�v�{�[�h�ɃA�h���X���R�s�[���܂���"),_T("Info"),MB_OK|MB_ICONINFORMATION);
				}
			}
			
			// URL ���N��
			if (g_Settings.count(L"open_browser") && g_Settings[L"open_browser"] == L"yes") {
				execUrl(result.c_str());
			}

			// ���O��������
			if (
				g_Settings.count(L"write_log") && g_Settings[L"write_log"] == L"yes" &&
				g_Settings.count(L"log_filename") && g_Settings[L"log_filename"].length() > 0
			) {
				char filename[1024];
				const wchar_t* from = g_Settings[L"log_filename"].c_str();
				wcstombs(filename , from, 1024);
				writeLog(result.c_str(), filename);
			}

			return TRUE;
		}
	} else {
		// �A�b�v���[�h���s...
		MessageBox(hwnd, _T("Failed to upload"), szTitle, MB_ICONERROR | MB_OK);
	}

	return FALSE;

}

// ���O�t�@�C����������
BOOL writeLog(const char* pcUrl, const char* filename)
{
	FILE		*fp;
	char		acWriteBuf[UPLOAD_LOG_LINE_SIZE];
	int			iRet;
	time_t		tmNow;
	struct tm	*pstNow;

	memset(acWriteBuf, 0, sizeof(acWriteBuf));

	// ���O�t�@�C���I�[�v��
	fp = fopen(filename, "a");
	if (fp == NULL) {
		return FALSE;
	}

	// ���ݎ����擾
	tmNow = time(NULL);
	pstNow = localtime(&tmNow);

	// ���O�쐬
	sprintf(acWriteBuf, "[%04d/%02d/%02d %02d:%02d:%02d] %s\n",
		pstNow->tm_year + 1900,
		pstNow->tm_mon + 1,
		pstNow->tm_mday,
		pstNow->tm_hour,
		pstNow->tm_min,
		pstNow->tm_sec,
		pcUrl
	);

	// ��������
	iRet = fputs(acWriteBuf, fp);
	if (iRet == EOF) {
		fclose(fp);
		return FALSE;
	}

	fclose(fp);

	return TRUE;
}