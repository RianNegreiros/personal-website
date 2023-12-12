import Link from "next/link";
import { useAuth } from "../contexts/AuthContext";

export default function NewLinks({ pathname }: { pathname: string }) {
  const { isAdmin } = useAuth();
  if (isAdmin) {
    if (pathname === "/posts") {
      return (
        <div>
          <Link
            href="/posts/new"
            className="text-sm px-4 py-2 rounded-lg border-2 border-teal-500 text-gray-500 hover:text-white hover:border-teal-600"
          >
            Criar Post
          </Link>
        </div>

      );
    } else if (pathname === "/projects") {
      return (
        <div>
          <Link
            href="/posts/new"
            className="text-sm px-4 py-2 rounded-lg border-2 border-teal-500 text-gray-500 hover:text-white hover:border-teal-600"
          >
            Criar Post
          </Link>
        </div>
      );
    }
  }
}