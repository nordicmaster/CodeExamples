import sys
import os
import xml.etree.ElementTree as ET

def update_valves(project_folder: str):
    print("project folder " + project_folder)
    project_parts_names = []
    specs = []
    exclude_specs = ["CustomParts Imperial", "CustomParts Metric", "EXIST", "Instrumentation Imperial", "Instrumentation Metric", "PipeSupportsSpec", "PlaceHolder Imperial", "PlaceHolder Metric"]
    projects_dirs = ["Q:\\2018", "Q:\\2019", "Q:\\Collaboration"]
    filters = {'1':'VGF', '2':'VGT', '3':'VCLF,VCSF', '4':'VBF', '5':'VBT'}
    for projects_dir in projects_dirs:
        if project_folder not in os.listdir(projects_dir):
            continue
        for specname in os.listdir(os.path.join(projects_dir, project_folder, 'Spec Sheets')):
            if os.path.isfile(os.path.join(projects_dir, project_folder, 'Spec Sheets', specname)):
                if specname[0:-5] in exclude_specs:
                    continue
                if specname[0:-5] in specs:
                    continue
                specs.append(specname[0:-5])
        for filename in os.listdir(os.path.join(projects_dir, project_folder)):
            if os.path.isfile(os.path.join(projects_dir, project_folder, filename)):
                continue
            if project_folder in filename:
                project_parts_names.append(filename)
        for app_name in os.listdir(os.path.join(projects_dir, project_folder, 'Applications')):
            if "P3DUpdateValves" in app_name:
                parser = ET.XMLParser(target = ET.TreeBuilder(insert_comments = True))
                tree = ET.parse(os.path.join(projects_dir, project_folder, 'Applications', app_name, 'Settings.xml'), parser)
                root = tree.getroot()
                
                root.find('Projects').append(ET.Element('Project', {'projectfoldername':project_folder,
                                                                    'schemaname':"Ремонт",
                                                                    'format': project_folder + "-ТМ.ОЛ{0}" }))
                for part in project_parts_names:
                    root.find('ProjectParts').append(ET.Element('ProjectPart', {'projectpartname':part,
                                                                                'format': part + ".ОЛ{0}" }))
                                                                                
                for i in range(1,6):
                    elem = root.find('.//Element[@globalnumber="' + str(i) + '"]')
                    filters_i = filters.get(str(i)).split(',')
                    for spec in specs:
                        for filter_i in filters_i:
                            elem.append(ET.Element('Filter', {'compatiblestandard': filter_i + "-" + spec}))
                tree.write(os.path.join(projects_dir, project_folder, 'Applications', app_name, 'Settings.xml'), encoding='UTF-8')
                
    print("Parts:")
    print(project_parts_names)
    print("Specs:")
    print(specs)

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("no argument passed")
    else:
        update_valves(sys.argv[1])
