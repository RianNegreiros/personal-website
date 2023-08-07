"use client"

import { ThemeProvider } from "next-themes"
import { ReactNode } from "react"
import { AuthProvider } from "../contexts/AuthContext";

export function Providers({ children }: { children: ReactNode }) {
  return (
    <ThemeProvider attribute="class">
      <AuthProvider>
        {children}
      </AuthProvider>
    </ThemeProvider>
  );
}