"use client"

import Image from "next/image"
import { getProjects } from "../utils/api";
import { Project } from "../models";
import { useEffect, useState } from "react";
import Link from "next/link";
import Loading from "../components/Loading";

async function getData() {
  return await getProjects();
}

export default function Projects() {
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<Project[]>([]);

  useEffect(() => {
    async function fetchData() {
      const projects = await getData();
      setData(projects.data);
      setIsLoading(false);
    }
    fetchData();
  }, []);

  return (
    <div className="divide-y divide-gray-200 dark:divide-gray-700">
      <div className="space-y-2 pt-6 pb-8 md:space-y-5">
      </div>
      {isLoading ? (
        <Loading />
      ) : (
        <div className="grid gap-y-8 sm:gap-6 sm:grid-cols-2 md:gap-6 lg:grid-cols-3 lg:gap-10 pt-8">
          {data.map((project) => (
            <article
              key={project.id}
              className="overflow-hidden dark:border-zinc-600 rounded-lg border border-gray-100 bg-white shadow-lg dark:bg-black dark:shadow-gray-700 shadow-dracula-pink-100"
            >
              <div className="h-56 w-full relative">
                <Image
                  fill
                  sizes="100%"
                  priority
                  src={project.imageUrl}
                  alt="Image of the project"
                  className="w-full h-full object-cover"
                />
              </div>

              <div className="p-4 sm:p-6">
                <Link href={project.url} target="_blank">
                  <h3 className="text-lg font-medium text-gray-900 dark:text-white">
                    {project.title}
                  </h3>
                </Link>

                <p className=" line-clamp-3 mt-2 text-sm leading-relaxed text-gray-500 dark:text-gray-400">
                  {project.overview}
                </p>

                <Link
                  href={project.url}
                  target="_blank"
                  className="cursor-pointer group mt-4 inline-flex items-center gap-1 text-sm font-medium text-dracula-pink"
                >
                  Saiba mais
                  <span className="block transition-all group-hover:ms-0.5">
                    &rarr;
                  </span>
                </Link>
              </div>
            </article>
          ))}
        </div>
      )}
    </div>
  );
}