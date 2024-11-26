# Expense Tracker

## Overview
The Expense Tracker is a web-based application designed to help users manage their finances effectively by categorizing and tracking income and expenses. Built with ASP.NET Core MVC, the application uses Entity Framework Core for data access and Syncfusion for interactive data visualization.
![5346133256225024366](https://github.com/user-attachments/assets/d684e43a-f688-4eed-9fd1-3f2ef81dc9ba)
## Features
- **Category Management**: Create, edit, and delete categories for income and expenses.
- **Transaction Tracking**: Record and view income and expense transactions.
- **Interactive Charts**: Visualize financial data with pie charts, spline charts, and more.
- **Responsive Design**: Accessible on multiple devices with a user-friendly interface.

![5346133256225024367](https://github.com/user-attachments/assets/94026b09-559c-41c7-b51c-03b2285394af)

## Technology Stack
- **Frontend**: HTML5, CSS3, JavaScript, Syncfusion components.
- **Backend**: ASP.NET Core MVC.
- **Database**: SQL Server using Entity Framework Core.
- **APIs**: Syncfusion for chart rendering and data presentation.

![5346133256225024365](https://github.com/user-attachments/assets/4160e48e-aaf2-46bd-9326-c84babceaba4)

## Project Structure
### Models
1. **Category**:
   - `CategoryId`: Primary key.
   - `Type`: Type of category (Income or Expense).
   - `Title`: Name of the category.
   - `Icon`: Icon representing the category.

2. **Transaction**:
   - `TransactionId`: Primary key.
   - `CategoryId`: Foreign key linking to Category.
   - `Amount`: Transaction amount.
   - `Date`: Date of the transaction.
   - `Note`: Optional notes about the transaction.

![5346133256225024362](https://github.com/user-attachments/assets/44befa73-030c-482e-8e55-c54693193562)

## Usage
1. **Add Categories**:
   - Navigate to the Categories page.
   - Click "+ New Category" to add a category.
2. **Add Transactions**:
   - Navigate to the Transactions page.
   - Record income or expenses by selecting a category and providing details.
3. **View Dashboard**:
   - View your financial summary, charts, and recent transactions.

![2](https://github.com/user-attachments/assets/f28dc5a2-6181-4297-ae6c-8bea99ab6c3e)

## Acknowledgments
- **Syncfusion**: For providing powerful UI components.
- **Entity Framework Core**: For seamless database integration.

![5346133256225024369](https://github.com/user-attachments/assets/4ad7d9b3-be10-44bf-b654-f5f6ee1983cd)
