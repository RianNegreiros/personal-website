"use client"

import Link from "next/link";
import ThemeButton from "./ThemeButton";
import AuthLinks from "./AuthLinks";
import { useAuth } from "../contexts/AuthContext";

export default function Navbar() {
  const { isAuthenticated } = useAuth();

  return (
    <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8">
      <div className="flex justify-between h-16">
        <div className="flex justify-between items-center w-full">
          <Link href="/">
            <h1 className="text-2xl font-medium cursor-pointer">
              Rian <span className="text-teal-500">Blog</span>
            </h1>
          </Link>

          <div className="flex items-center space-x-4">

            <AuthLinks userLogged={isAuthenticated} />

            <ThemeButton />
          </div>
        </div>
      </div>
    </div>
  );
}
