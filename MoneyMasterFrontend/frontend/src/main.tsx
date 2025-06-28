import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { AuthProvider } from "./Context/AuthContext.tsx";
import { AccountProvider } from "./Context/AccountContext.tsx";
import { TransactionProvider } from "./Context/TransactionContext.tsx";
import { CategoryProvider } from "./Context/CategoryContext.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AuthProvider>
      <AccountProvider>
        <TransactionProvider>
          <CategoryProvider>
            <App />
          </CategoryProvider>
        </TransactionProvider>
      </AccountProvider>
    </AuthProvider>
  </StrictMode>
);
