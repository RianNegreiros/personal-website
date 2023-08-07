import Link from "next/link"
import ThemeButton from "./ThemeButton"

export default function AuthLinks({ userLogged }: { userLogged: boolean }) {
  if (userLogged) {
    return (
      <>
        <Link href="/post/new" className="text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600">
          New Post
        </Link>
      </>
    )
  } else {
    return (
      <>
        <Link href="/signin" className="text-sm text-gray-500 hover:text-gray-700">
          Sign In
        </Link>
        <Link href="/signup" className="text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600">
          Sign Up
        </Link>
      </>
    )
  }
}