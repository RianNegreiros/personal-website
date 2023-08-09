import Link from "next/link"

export default function AuthLinks({ userLogged, pathname }: { userLogged: boolean, pathname: string }) {
  if (userLogged) {
    return (
      <>
        <Link
          href="/post/new"
          className={`${pathname === "/post/new"
            ? "border-teal-500 dark:text-white h-full inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium"
            : "border-transparent text-gray-500 dark:text-gray-300 dark:hover:text-white inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium"
            }`}
        >
          New Post
        </Link>
      </>
    )
  } else {
    return (
      <>
        <Link
          href="/signin"
          prefetch
          className={`${pathname === "/signin"
            ? "border-teal-500 dark:text-white h-full inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium"
            : "border-transparent text-gray-500 dark:text-gray-300 dark:hover:text-white inline-flex items-center px-1 pt-1 border-b-2 text-sm font-medium"
            }`}
        >
          Login
        </Link>
      </>
    )
  }
}