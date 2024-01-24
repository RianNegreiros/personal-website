"use client";

import { usePathname } from "next/navigation";

export default function Footer() {
  const pathname = usePathname();

  const paths = ["/signin", "/signup", "/terms/service", "/terms/privacy"]

  if (paths.includes(pathname)) {
    return null;
  }

  return (
    <footer className="mt-auto bg-white dark:bg-gray-900">
      <div className="mx-auto w-full max-w-screen-xl p-4 py-6 lg:py-8">

        <hr className="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
        <div className="sm:flex sm:items-center sm:justify-between">
          <span className="text-sm text-gray-500 sm:text-center dark:text-gray-400">© {new Date().getFullYear()} <a href="https://www.riannegreiros.dev" className="hover:underline">Rian Negreiros™</a>. Todos os direitos reservados.
          </span>

          <ul className="flex flex-wrap items-center mb-6 text-sm font-medium text-gray-500 sm:mb-0 dark:text-gray-400">
            <li>
              <a href="/terms/privacy" className="mr-4 hover:underline md:mr-6">Política Privacidade</a>
            </li>
            <li>
              <a href="/terms/service" className="mr-4 hover:underline md:mr-6">Termos de Serviço</a>
            </li>
          </ul>
        </div>
      </div>
    </footer>
  );
}