"use client"

import Link from "next/link";
import ThemeButton from "./ThemeButton";
import { useState } from "react";
import AuthLinks from "./AuthLinks";

export default function Navbar() {
  const [userLogged, setUserLogged] = useState(false);

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

            <AuthLinks userLogged={userLogged} />

            <ThemeButton />
          </div>
        </div>
      </div>
    </div>
  );
}
