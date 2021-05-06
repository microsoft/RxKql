const core = require('@actions/core');
const github = require('@actions/github');

const fs = require('fs');
const util = require('util');

const writefile = util.promisify(fs.writeFile);
const exec = util.promisify(require('child_process').exec);
const readdir = util.promisify(fs.readdir);

async function generatepfx(cert) {
  try {
    const secretcert = Buffer.from(core.getInput('certificate'), 'base64');
    await writefile(cert, secretcert);
    return true;
  }
  catch(err)
  {
    console.log(err);
    return false;
  }
}

async function getpackage(dir) {
  try {
    const fileNames = await readdir(dir);
    let nugetPackage = '';
    await Promise.all(fileNames.map(async (fileName) => {
      const extension = fileName.split('.').pop();
      if(extension == 'nupkg') {
        nugetPackage = `${dir}\\${fileName}`;
      }
    }))
    return nugetPackage;
  }
  catch (err)
  {
    console.log(err);
    return null;
  }
}

async function sign(package, cert) {
  const key = core.getInput('key');
  try {
    const { stdout, stderr } = await exec(`nuget sign "${package}" -Timestamper http://timestamp.digicert.com -CertificatePath "${cert}" -CertificatePassword ${key}`);
    if(stderr) {
      core.setFailed(stderr);
      return false;
    }
    else {
      console.log(stdout);
      return true;
    }
  }
  catch (err)
  {
    core.setFailed(err);
    return false;
  }
}

async function run() {
  try {
    // prepare pfx for signtool
    const cert = `${process.env.temp}.pfx`;
    if (await generatepfx(cert)) {}

    // get package and sign
    const dir = core.getInput('directory');
    const package = await getpackage(dir);
    if (await sign(package, cert))
    {
      console.log("Signed Nuget Package!");
    }
  }
  catch (err) {
    core.setFailed(err);
  }
}

run();