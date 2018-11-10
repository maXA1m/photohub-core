// Const for files cache name
const FILES_CACHE = 'cache-files-v1';
// Array with file paths to cache
const FILES_TO_CACHE = [
    '/images/cover-background-1.jpg',
    '/images/cover-background-2.jpg',
    '/images/cover-background-3.jpg',
    '/images/cover-background-4.jpg',
    '/images/cover-background-5.jpg',
    '/images/cover-background-6.jpg',
    '/images/cover-background-7.jpg',
    '/images/cover-background-8.jpg',
    '/images/defaults/def-female-logo.png',
    '/images/defaults/def-male-logo.png',
    '/images/defaults/def-photo.png'
];

// 'Install' event handler, here is caching static assets
self.addEventListener('install', (event) => {
    event.waitUntil(
        caches
            .open(FILES_CACHE)
            .then((cache) => cache.addAll(FILES_TO_CACHE))
    );
});

// 'Fetch' event handler, here is using cached static assets
self.addEventListener('fetch', (event) => {
    // `respondWith()` for response without waiting for server
    event.respondWith(fromCache(event.request));
});

// Loading assets from cache
function fromCache(request) {
    return caches.open(FILES_CACHE).then((cache) =>
        cache.match(request).then((matching) =>
            matching || Promise.reject('no-match')
        ));
}