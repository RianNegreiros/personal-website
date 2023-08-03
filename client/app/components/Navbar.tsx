import Link from "next/link";
import ThemeButton from "./ThemeButton";

export default function Navbar() {
  return (
    <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8">
      <div className="flex justify-between h-16">
        <div className="flex justify-between items-center w-full">
          {/* Home link */}
          <Link href="/">
            <h1 className="text-2xl font-medium cursor-pointer">
              Rian <span className="text-teal-500">Blog</span>
            </h1>
          </Link>

          <div className="flex items-center space-x-4">
            <Link href="/signin" className="text-sm text-gray-500 hover:text-gray-700">
              Sign In
            </Link>
            <Link href="/signup" className="text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600">
              Sign Up
            </Link>
            
            <ThemeButton />
          </div>
        </div>
      </div>
    </div>
  );
}
