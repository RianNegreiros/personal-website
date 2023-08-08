import { Post } from "@/app/models";
import ReactMarkdown from "react-markdown";

async function getData(id: string) {
  const response = await fetch(process.env.NEXT_PUBLIC_API_URL + '/post/' + id);
  const data = await response.json();
  return data;
}

export default async function PostPage({
  params,
}: {
  params: { id: string };
}) {
  const data = (await getData(params.id)) as Post;

  return (
    <div className="xl:divide-y xl:divide-gray-200 xl:dark:divide-gray-700">
      <header className="pt-6 xl:pb-6">
        <div className="space-y-1 text-center">
          <div className="space-y-10">
            <div>
              <p className="text-base font-medium leading-6 text-teal-500">
                {new Date().toISOString().split("T")[0]}
              </p>
            </div>
          </div>

          <div>
            <h1 className="text-3xl font-extrabold leading-9 tracking-tight text-gray-900 dark:text-gray-100 sm:text-4xl sm:leading-10 md:text-5xl md:leading-14">
              {data.title}
            </h1>
          </div>
        </div>
      </header>

      <div className="divide-y divide-gray-200 pb-7 dark:divide-gray-700 xl:divide-y-0">
        <div className="divide-y divide-gray-200 dark:divide-gray-700 xl:col-span-3 xl:row-span-2 xl:pb-0">
          <div className="prose max-w-none pb-8 pt-10 dark:prose-invert prose-lg">
            <ReactMarkdown>{data.body}</ReactMarkdown>
          </div>
        </div>
      </div>
    </div>
  )
}