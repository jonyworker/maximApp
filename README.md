# maximApp 專案說明（MAUI + SharedUI + WebHost）

本專案採用 **MAUI + Blazor + SharedUI + WebHost** 的分層架構，
目標是讓 **UI 設計、樣式、路由完全集中在 SharedUI**，
而 MAUI 與 WebHost 僅作為 Host 與驗證工具，確保專案長期可維護、可擴充、可交接。

---

## 一、專案架構總覽

```text
maximApp.sln
│
├─ maximApp                 # MAUI Host（產品 App）
│   ├─ MainPage.xaml        # BlazorWebView 掛載 SharedUI
│   ├─ App.xaml
│   ├─ MauiProgram.cs
│   ├─ Platforms/
│   ├─ Resources/
│   └─ wwwroot/
│       └─ index.html       # MAUI HostPage（入口）
│
├─ maximApp.SharedUI        # 核心 UI / Layout / Router
│   ├─ Components/
│   │   ├─ SharedApp.razor  # SharedUI 根元件
│   │   ├─ Routes.razor     # Router（唯一）
│   │   ├─ MauiRoot.razor   # MAUI 專用 Root（薄封裝）
│   │   ├─ Pages/
│   │   └─ Layout/
│   ├─ wwwroot/
│   │   └─ css/
│   │       └─ app.css      # Tailwind 輸出（不進版控）
│   └─ maximApp.SharedUI.csproj
│
├─ maximApp.WebHost         # 開發 / 驗證用 Web Host
│   ├─ Program.cs
│   ├─ Components/
│   │   └─ App.razor        # 掛載 <SharedApp />
│   └─ wwwroot/
│
└─ package.json             # Tailwind / 前端工具
```

---

## 二、各專案職責分工（非常重要）

### 1️⃣ maximApp（MAUI Host）

**責任：**

* 提供原生 App 外殼
* 掛載 BlazorWebView
* 載入 SharedUI 作為畫面來源

**不負責：**

* UI Layout
* CSS / 樣式
* Router / Page 定義

> maximApp =「殼」

---

### 2️⃣ maximApp.SharedUI（核心）

**責任：**

* 所有 UI / Layout / Page
* 唯一 Router 來源
* Tailwind CSS
* 與平台無關的畫面設計

**原則：**

* 不依賴 MAUI
* 不依賴 WebHost
* 可同時被 MAUI 與 WebHost 掛載

> SharedUI =「產品本體」

---

### 3️⃣ maximApp.WebHost（開發 / 驗證工具）

**責任：**

* 本機開發時快速預覽 SharedUI
* 驗證 Router / 互動 / Tailwind

**不負責：**

* 真實產品邏輯
* UI 結構設計

> WebHost =「測試跑道」

---

## 三、開發指令（請照這套）

### 1️⃣ 第一次安裝

```bash
npm install
```

---

### 2️⃣ 日常開發（推薦）

```bash
npm run dev
```

此指令會同時：

* 啟動 Tailwind watch（輸出到 SharedUI）
* 啟動 WebHost（支援 Hot Reload）

開啟瀏覽器：

```text
http://localhost:5123
```

---

### 3️⃣ 只編譯 Tailwind（發佈前）

```bash
npm run tw:build:min
```

---

### 4️⃣ MAUI 執行

* 使用 Visual Studio
* 選擇目標平台（Windows / Android / iOS）
* 直接 F5

---

## 四、CSS / Tailwind 規範

* `app.css` **不進 Git**
* 由 Tailwind 即時產生
* `.gitignore` 已忽略

開發時：

```bash
npm run dev
```

---

## 五、路由與互動規範

* Router **只存在於 SharedUI**
* WebHost / MAUI 不定義路由
* 若需互動（click / nav）：

  * WebHost 使用 `@rendermode="InteractiveServer"`
  * MAUI 由 BlazorWebView 處理

---

## 六、誰該改哪裡（團隊守則）

### 🎨 UI / 設計人員

* 只改 `maximApp.SharedUI`
* 不碰 MAUI / WebHost

---

### 🧠 邏輯 / 系統工程師

* MAUI 平台整合
* Native API
* 系統層互動

---

### 🧪 開發 / 驗證

* 使用 WebHost 預覽
* 不在 WebHost 寫 UI

---

## 七、重要原則（請一定遵守）

* ❌ 不在 maximApp 寫 Razor UI
* ❌ 不在 WebHost 寫 Layout / CSS
* ✅ SharedUI 是唯一畫面來源
* ✅ WebHost 只是工具

---

## 八、常見問題

### Q：為什麼不用單一 MAUI Blazor 專案？

A：為了清楚分離 UI 與平台，避免 UI 被 MAUI 綁死。

---

### Q：WebHost 之後能刪嗎？

A：能，但不建議。它是非常好的驗證工具。

---

## 九、狀態

此 README 對應 commit：

```text
chore: webhost cleanup complete
```

---

> 本文件為專案結構與流程的唯一正式說明文件。
