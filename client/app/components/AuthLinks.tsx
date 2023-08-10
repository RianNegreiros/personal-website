import Link from "next/link"

export default function AuthLinks({ isAdmin, pathname }: { isAdmin: boolean, pathname: string }) {
  if (isAdmin) {
    if (pathname === "/blog") {
      return (
        <Link
          href="/post/new"
          className="text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600"
        >
          New Post
        </Link>
      );
    } else if (pathname === "/projects") {
      return (
        <Link
          href="/projects/new"
          className="text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600"
        >
          New Project
        </Link>
      );
    }
  }

  return null;
}