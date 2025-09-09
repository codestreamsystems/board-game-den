# Board Game Den

A simple web application for sales representatives to view a list of board game products with pricing information. The system consists of an Angular frontend and a .NET Api backend.

## What This Project Does

- Displays a table of board game products from suppliers, with front end paging, filtering and sorting using AGGrid component
- Shows product thumbnails, names, and pricing
- Calculates "Our Sale Price" by adding 10% to the supplier's sale price
- Allows searching and filtering of products
- Uses simple forntend caching to improve performance

## What This Project Does NOT HAVE

Due to the nature of this exercise, it's simplicity and time constraints, this project has no unit, integration or front end tests

However, it's important to note that in a real-world production environment, comprehensive testing would be absolutely essential. A proper implementation would include unit tests for all services, components, and business logic; integration tests for API endpoints and data flow; end-to-end tests for critical user journeys; and performance tests for load handling and response times. Testing is a crucial part of professional software development, ensuring code quality, preventing regressions, and maintaining system reliability. This project's lack of tests is purely for demonstration purposes and should not be considered a best practice. In professional settings, I would advocate for test-driven development (TDD) and maintain high test coverage to ensure robust, maintainable code.

## Prerequisites

Before running this project, make sure you have installed:

- **Node.js** (version 18 or higher) - [Download here](https://nodejs.org/)
- **.NET 9 SDK** - [Download here](https://dotnet.microsoft.com/download)
- **Git** - [Download here](https://git-scm.com/)

## Project Structure

```
board-game-den/
├── board-game-den-app/     # Angular Frontend
└── board-game-den-server/  # .NET API Backend
```

## Getting Started

### 1. Clone the Project

```bash
git clone https://github.com/codestreamsystems/board-game-den.git
cd board-game-den
```

### 2. Set Up the Backend API

**Navigate to the API folder:**
```bash
cd board-game-den-server
```

**Install dependencies and run:**
```bash
dotnet restore
dotnet build
dotnet run
```

The API will start at: `http://localhost:5054`

**Important API Endpoints:**
- `http://localhost:5054` - API information
- `http://localhost:5054/api/products` - Get all products
- `http://localhost:5054/swagger` - API documentation

### 3. Set Up the Frontend

**Open a new command prompt and navigate to the frontend folder:**
```bash
cd board-game-den-app
```

**Install dependencies:**
```bash
npm install
```

**Configure the API URL:**

Edit the file `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiBaseUrl: 'http://localhost:5054',  // Change this if your API runs on a different port
  apiKey: 'BGD-2025-DEV-KEY-12345' // Must match the API key in the API configuration
};
```

**Run the frontend:**
```bash
ng serve
```

The website will open at: `http://localhost:4200`

## Environment Configuration

### Frontend Environment Variables

**Development (`src/environments/environment.ts`):**
- `apiBaseUrl` - The URL where your .NET API is running (default: `http://localhost:5054`)
- `apiKey` - Must match the API key in the API configuration

### Backend Configuration

## API Key Configuration

This application requires an API key for security.

Set your API key in the configuration files:

**Development (`appsettings.Development.json`):**
```json
{
  "ApiKey": "BGD-2025-DEV-KEY-12345"
}
```

The .NET API uses default settings, but you can modify:

**In `appsettings.json` or `appsettings.Development.json`:**
- Logging levels
- CORS settings (currently allows Angular app on port 4200)
- ApiKey for security

## How to Use

1. **Start both applications** (API first, then frontend)
2. **Open your browser** to `http://localhost:4200`
3. **Search for products** using the search box
4. **View pricing** - Sale Price vs Our Sale Price (Sale Price + 10%)
5. **Use the clear button** (X) to reset your search

### Using Swagger with API Key:

1. Open Swagger UI at `http://localhost:5054/swagger`
2. Click the **"Authorize"** button (🔒 icon)
3. Enter your API key: `BGD-2025-DEV-KEY-12345`
4. Click **"Authorize"**
5. Test any endpoint - the API key will be included automatically

## Features

- **Product Display**: Shows board games with images, titles, and pricing
- **Search**: Filter products by name
- **Pricing**: Automatically calculates markup (10% added to sale price)
- **Caching**: Faster loading after first API call
- **Responsive Design**: Works on desktop and mobile
- **Professional UI**: Clean design suitable for sales representatives

## Troubleshooting

### API Not Working
- Make sure .NET 9 SDK is installed
- Check if port 5054 is available
- Look for errors in the command prompt where you ran `dotnet run`

### Frontend Not Working
- Make sure Node.js is installed
- Check if port 4200 is available
- Verify the `apiBaseUrl` in environment files matches your API
- Look for errors in the command prompt where you ran `ng serve`

### No Data Showing
- Verify the API is running and accessible at `http://localhost:5054/api/products`
- Check browser developer tools (F12) for any network errors
- Ensure CORS is properly configured in the API

### Search Not Working
- Make sure you click the "Search" button after typing
- Use the clear button (X) to reset and show all products

## Data Source

The application fetches board game data from: `http://myboardgamelibrary.com/boardgames.json`

If this external source is unavailable, the API will return an empty data set.

## Development Notes

- **Currency**: Prices are displayed in South African Rands (R)
- **Caching**: Data is cached for 5 minutes to improve performance
- **Framework**: Angular 17+ with standalone components
- **Styling**: Tailwind CSS for responsive design
- **Grid**: AG Grid for professional data table display

## Need Help?

1. Check that both the API and frontend are running
2. Verify your environment configuration
3. Look at the browser console (F12) for error messages
4. Check the command prompt outputs for any error messages

## Security Notes:

- **Not production-grade security** - This is a simple implementation
- **Store keys securely** in production (use environment variables, not config files)
- **Use HTTPS** in production
- **Rotate keys regularly** in production environments